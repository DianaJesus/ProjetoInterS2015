﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Web.Hosting;
using System.Xml;


namespace PiPorDataWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {

        private Dictionary<string, Utilizador> utilizadores;
        private Dictionary<string, Token> tokens;
        private static string FILEPATH;


        public Service1()
        {
            this.utilizadores = new Dictionary<string, Utilizador>();
            this.tokens = new Dictionary<string, Token>();

            // default administrator
            utilizadores.Add("admin", new Utilizador("admin", "admin", true));
            FILEPATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "XmlTestexml.xml");
        }


        private class Token
        {
            private string value;
            private long timeout;
            private Utilizador utilizador;
            public Token(Utilizador utilizador)
            : this(utilizador, 240000) // token válido por 4 minutos
            { }
            public Token(Utilizador utilizador, long timeout)
            {
                this.value = Guid.NewGuid().ToString();
                this.timeout = Environment.TickCount + timeout;
                this.utilizador = utilizador;
            }
            public string Value
            {
                get { return value; }
            }
            public long Timeout
            {
                get { return timeout; }
            }
            public Utilizador Utilizador
            {
                get { return utilizador; }
            }
            public string Username

            {
                get { return utilizador.Username; }
            }
            public void UpdateTimeout()
            {
                UpdateTimeout(240000); // token renovado por 4 minutos
            }
            public void UpdateTimeout(long timeout)
            {
                this.timeout = Environment.TickCount + timeout;
            }
            public Boolean isTimeoutExpired()
            {
                return Environment.TickCount > timeout;
            }
        }


        public void SignUp(Utilizador utilizador, string token)
        {
            checkAuthentication(token, true);
            if (utilizadores.Keys.Contains(utilizador.Username))
            {
                throw new ArgumentException("ERROR: username already exists: " + utilizador.Username);
            }
            utilizadores.Add(utilizador.Username, utilizador);
        }
        public string LogIn(string username, string password)
        {
            cleanUpTokens();
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password) &&
           password.Equals(utilizadores[username].Password))
            {
                Token tokenObject = new Token(utilizadores[username]);
                tokens.Add(tokenObject.Value, tokenObject);
                return tokenObject.Value;
            }
            else
            {
                throw new ArgumentException("ERROR: invalid username/password combination.");
            }
        }
        public void LogOut(string token)
        {

            tokens.Remove(token);
            cleanUpTokens();
        }
        public bool IsAdmin(string token)
        {
            return tokens[token].Utilizador.Admin;
        }
        public bool IsLoggedIn(string token)
        {
            bool res = true;
            try
            {
                checkAuthentication(token, false);
            }
            catch (ArgumentException)
            {
                res = false;
            }
            return res;
        }
        private void cleanUpTokens()
        {
            foreach (Token tokenObject in tokens.Values)
            {
                if (tokenObject.isTimeoutExpired())
                {
                    tokens.Remove(tokenObject.Username);
                }
            }
        }
        private Token checkAuthentication(string token, bool mustBeAdmin)
        {
            Token tokenObject;
            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentException("ERROR: invalid token value.");
            }
            try
            {
                tokenObject = tokens[token];
            }
            catch (KeyNotFoundException)
            {
                throw new ArgumentException("ERROR: user is not logged in (expired session?).");
            }
            if (tokenObject.isTimeoutExpired())
            {
                tokens.Remove(tokenObject.Username);
                throw new ArgumentException("ERROR: the session has expired. Please log in again.");
            }
            if (mustBeAdmin && !tokens[token].Utilizador.Admin)
            {
                throw new ArgumentException("ERROR: only admins are allowed to perform this operation.");
            }
            tokenObject.UpdateTimeout();
            return tokenObject;
        }

        public int GetNumFunc(string token)
        {
            var soma = 0;
            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);
           // List<Funcionario> funcionarios = new List<Funcionario>();
            foreach (XmlNode anosNode in doc.SelectNodes("//PessoalAoServico"))
            {
                foreach (XmlNode item in anosNode.ChildNodes)
                {
                    soma += Int32.Parse(item.InnerText);
                }
              //  Funcionario func = new Funcionario(null,soma);
              //  funcionarios.Add(func);
            }
            return soma;
        }

        public List<Funcionario> GetNumFunc(string dataInicio, string dataFim, string token)
        {
            var soma = 0;
            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);
            List<Funcionario> funcionarios = new List<Funcionario>();
            List<int> numeros = new List<int>();
            foreach (XmlNode anosNode in doc.SelectNodes("//PessoalAoServico"))
            {
                numeros.Add(Int32.Parse(anosNode.InnerText));
                foreach (var item in numeros)
                {
                    if(item.Equals(dataInicio) && item.Equals(dataFim))
                    {
                        foreach (XmlNode item1 in anosNode.ChildNodes)
                        {
                            soma += Int32.Parse(item1.InnerText);
                        }
                    }
                }
                /*if (Int32.Parse(anosNode.ChildNodes.ToString()).Equals(dataInicio) && Int32.Parse(anosNode.ChildNodes.ToString()).Equals(dataFim))
                {
                    foreach (XmlNode item in anosNode.ChildNodes)
                    {
                        soma += Int32.Parse(item.InnerText);
                    }

                }*/
                Funcionario func = new Funcionario(null, soma);
                funcionarios.Add(func);
            }
            return funcionarios;
        }



        public int GetNumFunc(string categoria, string token)
        {
           
            int var = 0;
           
            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);

            switch (categoria)
            {
                case "Medicos":

                    foreach (XmlNode anoNode in doc.SelectNodes("//PessoalAoServico"))
                    {
                        foreach (XmlNode item in anoNode.SelectSingleNode("Medicos"))
                        {
                            var += Int32.Parse(item.InnerText);
                        }
                    }

                    break;


                case "PessoalDeEnfermagem":

                    foreach (XmlNode anoNode in doc.SelectNodes("//PessoalAoServico"))
                    {
                        foreach (XmlNode item in anoNode.SelectSingleNode("PessoalDeEnfermagem"))
                        {
                            var += Int32.Parse(item.InnerText);
                        }
                    }

                    break;


                case "Enfermeiros":

                    foreach (XmlNode anoNode in doc.SelectNodes("//PessoalAoServico"))
                    {
                        foreach (XmlNode item in anoNode.SelectSingleNode("Enfermeiros"))
                        {
                            var += Int32.Parse(item.InnerText);
                        }
                    }
                    break;

                case "TecnicosDiagnosticoTerapeutica":
                    foreach (XmlNode anoNode in doc.SelectNodes("//PessoalAoServico"))
                    {
                        foreach (XmlNode item in anoNode.SelectSingleNode("TecnicosDiagnosticoTerapeutica"))
                        {
                            var += Int32.Parse(item.InnerText);
                        }
                    }
                    break;

                default:
                    throw new ArgumentNullException("Erro");
                    
                    


                    
            }

            /*foreach (XmlNode anoNode in anosNode)
            {

               
                XmlNode catMedNode = anoNode.SelectSingleNode("Medicos");
                XmlNode catEnfNode = anoNode.SelectSingleNode("PessoalDeEnfermagem");
                XmlNode catEnferNode = anoNode.SelectSingleNode("Enfermeiros");
                XmlNode catTerNode = anoNode.SelectSingleNode("TecnicosDiagnosticoTerapeutica");
                //  var += Int32.Parse(catMedNode.InnerText) + Int32.Parse(catEnferNode.InnerText) + Int32.Parse(catEnferNode.InnerText) + 
                //   Int32.Parse(catTerNode.InnerText) ;
                var += Int32.Parse(catMedNode.InnerText);
                var1 += Int32.Parse(catEnfNode.InnerText);
                var2 += Int32.Parse(catEnferNode.InnerText);
                var3 += Int32.Parse(catTerNode.InnerText);
                soma = var + var1 + var2 + var3;
                Funcionario func = new Funcionario(soma);

                funcionarios.Add(func);
            }*/
            return var;


        }




        /*public string GetData(int value)
        {


            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }*/

    }


}
