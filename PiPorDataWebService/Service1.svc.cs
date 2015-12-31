using System;
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
        //ceijfeijfiejfeijfije

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

       public List<Funcionario> GetNumFunc(int dataInicio, int dataFim, string token)
        {
            double valor = 0;
            double valorMedico = 0, valorEnfermagem = 0, valorEnfermeiros = 0, valorTer = 0;
            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);

            List<Funcionario> funcionarios = new List<Funcionario>();




            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
              
                for (int i = dataInicio; i <= dataFim; i++)
                {
                 
                  
                    XmlNode medicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Medicos");
                    if (medicos == null)
                    {
                        valorMedico = 0;
                    }else
                    {
                        valorMedico = Convert.ToDouble(medicos.InnerText);
                    }
                    XmlNode pessoalDeEnfermagem = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/PessoalDeEnfermagem");

                    if (pessoalDeEnfermagem == null)
                    {
                        valorEnfermagem = 0;
                    }else
                    {
                        valorEnfermagem = Convert.ToDouble(pessoalDeEnfermagem.InnerText);
                    }
                
                    XmlNode enfermeiros = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Enfermeiros");
                    if(enfermeiros== null)
                    { 
                    
                        valorEnfermeiros = 0;
                    }
                    else
                    {
                        valorEnfermeiros = Convert.ToDouble(enfermeiros.InnerText);
                    }
                    XmlNode tecnicosDiagnosticoTerapeutica = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/TecnicosDiagnosticoTerapeutica");
                    if(tecnicosDiagnosticoTerapeutica  == null)
                    {
                        valorTer = 0;
                    }else
                    {
                        valorTer = Convert.ToDouble(tecnicosDiagnosticoTerapeutica.InnerText);
                    }
                    valor = Math.Round(valorMedico + valorEnfermagem + valorEnfermeiros + valorTer, 0);

                    if(valor != 0)
                    {
                        Funcionario func = new Funcionario(i, 0.0, valor, 0.0, 0.0);
                        funcionarios.Add(func);
                    }
                   
                   


                }

            }




            
            return funcionarios;


        }




        
        public List<Acao> GetNumAcoesCategoria(int dataInicio, int dataFim, string categoria, string token) 
        {
            double valor = 0;
            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);


            List<Acao> acoes = new List<Acao>();

            switch (categoria)
            {
                case "Consultas":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode consultas = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Consultas/Hospitais");

                            if (consultas == null)
                            {
                                valor = 0;
                            }
                            else
                            {
                                valor = Math.Round(Convert.ToDouble(consultas.InnerText), 0);
                            }


                            if (valor != 0)
                            {
                                Acao acao = new Acao(i, 0.0, valor);
                                acoes.Add(acao);
                            }
                            
                            
                        }
                    }

                    break;


                case "Internamentos":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode internamentos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Internamentos/Hospitais");

                            if (internamentos == null)
                            {
                                valor = 0;
                            }
                            else
                            {
                                valor = Math.Round(Convert.ToDouble(internamentos.InnerText), 0);
                            }


                            if (valor != 0)
                            {
                                Acao acao = new Acao(i, 0.0, valor);
                                acoes.Add(acao);
                            }
                            

                        }
                    }

                    break;


                case "Urgencias":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode urgencias = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Urgencias/Hospitais");

                            if (urgencias == null)
                            {
                                valor = 0;
                            }
                            else
                            {
                                valor = Math.Round(Convert.ToDouble(urgencias.InnerText), 0);
                            }


                            if (valor != 0)
                            {
                                Acao acao = new Acao(i, 0.0, valor);
                                acoes.Add(acao);
                            }
                            

                        }
                    }

                    break;

               

                default:
                    throw new ArgumentNullException("Erro");





            }
            return acoes;


        }




       /* public List<Acao> GetNumAcoes(int dataInicio, int dataFim, string token)
        {

            int valorC = 0;
            int valorI = 0;
            int valorU = 0;

            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);
            List<Acao> acoes = new List<Acao>();
            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
                for (int i = dataInicio; i <= dataFim; i++)
                {
                    XmlNode consultas = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Consultas/Hospitais");
                    XmlNode internamentos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Internamentos/Hospitais");
                    XmlNode urgencias = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Urgencias/Hospitais");
                    valorC = Convert.ToInt32(consultas.InnerText);
                    valorI = Convert.ToInt32(internamentos.InnerText);
                    valorU = Convert.ToInt32(urgencias.InnerText);
                    Acao acao = new Acao(i, valorC, valorI, valorU);
                    acoes.Add(acao);
               }

            }
            return acoes;


        }*/
       
        /*public List<Funcionario> GetNumFuncCategoria(int dataInicio, int dataFim, string token)
        {
            int valorM = 0;
            int valorE = 0;
            int valorT = 0;
            
            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);

            
            List<Funcionario> funcionarios = new List<Funcionario>();


            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
                for (int i = dataInicio; i <= dataFim; i++)
                {


                    
                    XmlNode medicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Medicos");

                    XmlNode enfermeiros = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Enfermeiros");

                    XmlNode tecnicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/TecnicosDiagnosticoTerapeutica");
                    valorM = Convert.ToInt32(medicos.InnerText);
                    valorE = Convert.ToInt32(enfermeiros.InnerText);
                    valorT = Convert.ToInt32(tecnicos.InnerText); 
                    Funcionario funcionario = new Funcionario(i, valorM, valorE, valorT);
                    funcionarios.Add(funcionario);
                    


                }

            }





            return funcionarios;


        }*/



        
        public List<Funcionario> GetNumFuncCategoriaS(int dataInicio, int dataFim, string categoria, string token)
        {
            double valor = 0;
            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);


            List<Funcionario> funcionarios = new List<Funcionario>();



            switch (categoria)
            {
                case "Medicos":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode medicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Medicos");


                            if (medicos == null)
                            {
                                valor = 0;
                            }
                            else
                            {
                                valor = Math.Round(Convert.ToDouble(medicos.InnerText), 0);
                            }


                            if (valor != 0)
                            {
                                Funcionario func = new Funcionario(i,0.0,valor, 0.0,0.0);
                                funcionarios.Add(func);
                            }
                            
                        }
                    }

                    break;


                case "PessoalDeEnfermagem":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode pessoalDeEnfermagem = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/PessoalDeEnfermagem");


                            if (pessoalDeEnfermagem == null)
                            {
                                valor = 0;
                            }
                            else
                            {
                                valor = Math.Round(Convert.ToDouble(pessoalDeEnfermagem.InnerText), 0);
                            }


                            if (valor != 0)
                            {
                                Funcionario func = new Funcionario(i, 0.0, valor, 0.0, 0.0);
                                funcionarios.Add(func);
                            }

                            
                        }
                    }

                    break;


                case "Enfermeiros":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode enfermeiros = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Enfermeiros");


                            if (enfermeiros == null)
                            {
                                valor = 0;
                            }
                            else
                            {
                                valor = Math.Round(Convert.ToDouble(enfermeiros.InnerText), 0);
                            }


                            if (valor != 0)
                            {
                                Funcionario func = new Funcionario(i, 0.0, valor, 0.0, 0.0);
                                funcionarios.Add(func);
                            }
                            
                        }
                    }

                    break;

                case "TecnicosDiagnosticoTerapeutica":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode tecnicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/TecnicosDiagnosticoTerapeutica");

                            if (tecnicos == null)
                            {
                                valor = 0;
                            }
                            else
                            {
                                valor = Math.Round(Convert.ToDouble(tecnicos.InnerText), 0);
                            }


                            if (valor != 0)
                            {
                                Funcionario func = new Funcionario(i, 0.0, valor, 0.0, 0.0);
                                funcionarios.Add(func);
                            }
                            
                        }
                    }

                    break;

                default:
                    throw new ArgumentNullException("Erro");





            }
            return funcionarios;


        }


        

        public List<Funcionario> GetMediaFuncionario(int dataInicio, int dataFim, string token)
        {
            double valor = 0.0, valorMedicos = 0.0, valorEnfermagem = 0.0, valorEnfermeiros = 0.0, valorTerapia = 0.0;
            double funcionariosTotal = 0.0;
            double despesaPessoal = 0.0;
            

            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);


            List<Funcionario> funcionarios = new List<Funcionario>();


            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
                for (int i = dataInicio; i <= dataFim; i++)
                {


                    
                    XmlNode medicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Medicos");

                    if (medicos == null)
                    {
                        valorMedicos = 0;
                    }
                    else
                    {
                        valorMedicos = Convert.ToDouble(medicos.InnerText);
                    }


                    XmlNode pessoalEnfermagem = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/PessoalDeEnfermagem");

                    if (pessoalEnfermagem == null)
                    {
                        valorEnfermagem = 0;
                    }
                    else
                    {
                        valorEnfermagem = Convert.ToDouble(pessoalEnfermagem.InnerText);
                    }

                    XmlNode enfermeiros = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Enfermeiros");

                    if (enfermeiros == null)
                    {
                        valorEnfermeiros = 0;
                    }
                    else
                    {
                        valorEnfermeiros = Convert.ToDouble(enfermeiros.InnerText);
                    }

                    XmlNode tecnicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/TecnicosDiagnosticoTerapeutica");

                    if (tecnicos == null)
                    {
                        valorTerapia = 0;
                    }
                    else
                    {
                        valorTerapia = Convert.ToInt32(tecnicos.InnerText);
                    }

                    XmlNode comPessoal = doc.SelectSingleNode("//Anos[@ano=" + i + "]/DespesaSns/ComPessoal");

                    if (comPessoal == null)
                    {
                        despesaPessoal = 0;
                    }
                    else
                    {
                        despesaPessoal = Convert.ToDouble(comPessoal.InnerText);
                    }

                    funcionariosTotal = valorMedicos + valorEnfermagem + valorEnfermeiros + valorTerapia;

                   


                    if (funcionariosTotal != 0)
                    {
                        valor = Math.Round(despesaPessoal / funcionariosTotal, 2);
                        Funcionario func = new Funcionario(i, valor,0.0,0.0,0.0);
                        funcionarios.Add(func);
                    }


                }

            }





            return funcionarios;


        }


        
        public List<Funcionario> GetPercentagemPessoal(int dataInicio, int dataFim, string token)
        {
            double valor = 0.0;
            double despesaPessoal = 0.0;
            double despesaTotal = 0.0;
            double valorPercentagem = 0.0;


           checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);


            List<Funcionario> funcionarios = new List<Funcionario>();




            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
                for (int i = dataInicio; i <= dataFim; i++)
                {



                    XmlNode pessoal = doc.SelectSingleNode("//Anos[@ano=" + i + "]/DespesaSns/ComPessoal");

                    if (pessoal == null)
                    {
                        despesaPessoal = 0;
                    }
                    else
                    {
                        despesaPessoal = Convert.ToDouble(pessoal.InnerText);
                    }

                    XmlNode total = doc.SelectSingleNode("//Anos[@ano=" + i + "]/DespesaSns/Total");

                    if (total == null)
                    {
                        despesaTotal = 0;
                    }
                    else
                    {
                        despesaTotal = Convert.ToDouble(total.InnerText);
                    }

                   

                    if (despesaTotal != 0 || despesaPessoal != 0)
                    {

                        valor = despesaPessoal / despesaTotal;

                        valorPercentagem = Math.Round(valor * 100, 2);
                        Funcionario func = new Funcionario(i, 0.0,0.0,valorPercentagem,0.0);
                        funcionarios.Add(func);
                    }

                }

            }





            return funcionarios;


        }


        
        public List<Medicamento> GetPercentagemMedicamentos(int dataInicio, int dataFim, string token)
        {
            double valor = 0.0, valorSns = 0.0, valorUtente = 0.0;
            double despesaMedicamentos = 0.0;
            double despesaTotal = 0.0;
            double valorPercentagem = 0.0;


            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);

            List<Medicamento> medicamentos = new List<Medicamento>();





            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
                for (int i = dataInicio; i <= dataFim; i++)
                {



                    XmlNode medicamentosSns = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EncargosComMedicamentos/DoSns");

                    if (medicamentosSns == null)
                    {
                        valorSns = 0;
                    }
                    else
                    {
                        valorSns = Convert.ToDouble(medicamentosSns.InnerText);
                    }


                    XmlNode medicamentosUtente = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EncargosComMedicamentos/DoUtente");

                    if (medicamentosUtente == null)
                    {
                        valorUtente = 0;
                    }
                    else
                    {
                        valorUtente = Convert.ToDouble(medicamentosUtente.InnerText);
                    }


                    XmlNode total = doc.SelectSingleNode("//Anos[@ano=" + i + "]/DespesaSns/Total");

                    if (total == null)
                    {
                        despesaTotal = 0;
                    }
                    else
                    {
                        despesaTotal = Convert.ToDouble(total.InnerText);
                    }


                    despesaMedicamentos = valorSns + valorUtente;




                    if (despesaMedicamentos !=0)
                    {
                      

                        valor = despesaMedicamentos / despesaTotal;
                        valorPercentagem = Math.Round(valor * 100, 2);
                        Medicamento medicamento = new Medicamento(i, valorPercentagem);
                        medicamentos.Add(medicamento);
                    }
                    






                }

            }





            return medicamentos;


        }


        
        public List<Cama> GetMediaCamas(int dataInicio, int dataFim, string token)
        {

            double hospitais = 0, valorGerais = 0, valorEspecializados = 0;
            double media = 0.0;
            


            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);


            List<Cama> camas = new List<Cama>();


            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
                for (int i = dataInicio; i <= dataFim; i++)
                {

                    

                    XmlNode hospitaisGerais = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Lotacao/HospitaisGerais");

                    if (hospitaisGerais == null)
                    {
                        valorGerais = 0;
                    }
                    else
                    {
                        valorGerais = Convert.ToDouble(hospitaisGerais.InnerText);
                    }

                    XmlNode hospitaisEspecializados = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Lotacao/HospitaisEspecialiazados");

                    if (hospitaisEspecializados == null)
                    {
                        valorEspecializados = 0;
                    }
                    else
                    {
                        valorEspecializados = Convert.ToDouble(hospitaisEspecializados.InnerText);
                    }





                    hospitais = valorGerais + valorEspecializados;

                    media = Math.Round(hospitais / 2, 2);


                    if (media != 0)
                    {
                        Cama cama = new Cama(i, media);
                        camas.Add(cama);
                    }
                    






                }

            }





            return camas;


        }



        
        public List<Funcionario> GetRacioFuncionariosEstabelecimentos(int dataInicio, int dataFim, string token)
        {
            double valor = 0.0, valorMedicos = 0.0, valorEnfermagem = 0.0, valorEnfermeiros = 0.0, valorTerapia = 0.0;
            double valorGerais = 0.0, valorEspecializados = 0.0, valorCentros = 0.0, valorExtensoes = 0.0;
            double funcionarios = 0.0;
            double estabelecimentos = 0.0;
   


            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);

            List<Funcionario> listaFuncionarios = new List<Funcionario>();





            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
                for (int i = dataInicio; i <= dataFim; i++)
                {



                    XmlNode medicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Medicos");

                    if (medicos == null)
                    {
                        valorMedicos = 0;
                    }
                    else
                    {
                        valorMedicos = Convert.ToDouble(medicos.InnerText);
                    }

                    XmlNode pessoalEnfermagem = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/PessoalDeEnfermagem");

                    if (pessoalEnfermagem == null)
                    {
                        valorEnfermagem = 0;
                    }
                    else
                    {
                        valorEnfermagem = Convert.ToDouble(pessoalEnfermagem.InnerText);
                    }

                    XmlNode enfermeiros = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Enfermeiros");

                    if (enfermeiros == null)
                    {
                        valorEnfermeiros = 0;
                    }
                    else
                    {
                        valorEnfermeiros = Convert.ToDouble(enfermeiros.InnerText);
                    }


                    XmlNode tecnicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/TecnicosDiagnosticoTerapeutica");

                    if (tecnicos == null)
                    {
                        valorTerapia = 0;
                    }
                    else
                    {
                        valorTerapia = Convert.ToDouble(tecnicos.InnerText);
                    }



                    XmlNode hospitaisGerais = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EstabelecimentosSaude/HospitaisGerais");

                    if (hospitaisGerais == null)
                    {
                        valorGerais = 0;
                    }
                    else
                    {
                        valorGerais = Convert.ToDouble(hospitaisGerais.InnerText);
                    }

                    XmlNode hospitaisEspecializados = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EstabelecimentosSaude/HospitaisEspecialiazados");

                    if (hospitaisEspecializados == null)
                    {
                        valorEspecializados = 0;
                    }
                    else
                    {
                        valorEspecializados = Convert.ToDouble(hospitaisEspecializados.InnerText);
                    }

                    XmlNode centrosSaude = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EstabelecimentosSaude/CentrosDeSaude");

                    if (centrosSaude == null)
                    {
                        valorCentros = 0;
                    }
                    else
                    {
                        valorCentros = Convert.ToDouble(centrosSaude.InnerText);
                    }

                    XmlNode extensoes = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EstabelecimentosSaude/ExtensoesCentroSaude");

                    if (extensoes == null)
                    {
                        valorExtensoes = 0;
                    }
                    else
                    {
                        valorExtensoes = Convert.ToDouble(extensoes.InnerText);
                    }


                    funcionarios = valorMedicos + valorEnfermagem + valorEnfermeiros + valorTerapia;

                    estabelecimentos = valorGerais + valorEspecializados + valorCentros + valorExtensoes;




                    


                    if (funcionarios != 0 || estabelecimentos != 0)
                    {

                        valor =Math.Round(funcionarios / estabelecimentos, 0);
                        
                        Funcionario func = new Funcionario(i, 0.0, 0.0,0.0,valor);
                        listaFuncionarios.Add(func);
                    }
                    




                }

            }





            return listaFuncionarios;


        }


       


        public List<Acao> GetPercentagemAcoes(int dataInicio, int dataFim, string categoria, string token)
        {
            double valor = 0.0, valorConsultas = 0.0, valorConsultasTotal = 0.0, valorInternamentos = 0.0, valorInternamentosTotal = 0.0;
            double valorUrgencias = 0.0, valorUrgenciasTotal = 0.0;
            double percentagem = 0.0;
            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);


            List<Acao> acoes = new List<Acao>();



            switch (categoria)
            {
                case "Consultas":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode consultas = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Consultas/CentrosSaude");

                            if (consultas == null)
                            {
                                valorConsultas = 0;
                            }
                            else
                            {
                                valorConsultas = Convert.ToDouble(consultas.InnerText);
                            }


                            XmlNode consultasTotal = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Consultas/Total");


                            if (consultasTotal == null)
                            {
                                valorConsultasTotal = 0;
                            }
                            else
                            {
                                valorConsultasTotal = Convert.ToDouble(consultasTotal.InnerText);
                            }


                          
                            

                            if (valorConsultas != 0 || valorConsultasTotal != 0)
                            {
                                valor = valorConsultas / valorConsultasTotal;
                                percentagem = Math.Round(valor * 100, 2);
                                Acao acao = new Acao(i, percentagem, 0.0);
                                acoes.Add(acao);
                            }
                           
                        }
                    }

                    break;


                case "Internamentos":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode internamentos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Internamentos/CentrosSaude");


                            if (internamentos == null)
                            {
                                valorInternamentos = 0;
                            }
                            else
                            {
                                valorInternamentos = Convert.ToDouble(internamentos.InnerText);
                            }

                            XmlNode internamentosTotal = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Internamentos/Total");


                            if (internamentosTotal == null)
                            {
                                valorInternamentosTotal = 0;
                            }
                            else
                            {
                                valorInternamentosTotal = Convert.ToDouble(internamentosTotal.InnerText);
                            }


                         
                            


                            if (valorInternamentos != 0 || valorConsultasTotal != 0)
                            {
                                valor = valorInternamentos / valorInternamentosTotal;
                                percentagem = Math.Round(valor * 100, 2);
                                Acao acao = new Acao(i, percentagem, 0.0);
                                acoes.Add(acao);
                            }
                            
                        }
                    }

                    break;


                case "Urgencias":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode urgencias = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Urgencias/CentrosSaude");


                            if (urgencias == null)
                            {
                                valorUrgencias = 0;
                            }
                            else
                            {
                                valorUrgencias = Convert.ToDouble(urgencias.InnerText);
                            }

                            XmlNode urgenciasTotal = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Urgencias/Total");


                            if (urgenciasTotal == null)
                            {
                                valorUrgenciasTotal = 0;
                            }
                            else
                            {
                                valorUrgenciasTotal = Convert.ToDouble(urgenciasTotal.InnerText);
                            }


                           
                            

                            if (valorUrgencias != 0 || valorUrgenciasTotal != 0)
                            {
                                valor = valorUrgencias / valorUrgenciasTotal;

                                percentagem = Math.Round(valor * 100, 2);
                                Acao acao = new Acao(i, percentagem, 0.0);
                                acoes.Add(acao);
                            }

                            
                        }
                    }

                    break;

                

                default:
                    throw new ArgumentNullException("Erro");





            }
            return acoes;


        }



        //cejsdnsjs


        public Boolean ReceberXml(string xml)
        {

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);
                xmlDoc.Save(FILEPATH);
                return true;
            }
            catch (Exception ex )
            {
                return false;
               
            }


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
