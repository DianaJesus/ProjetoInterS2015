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
            int valor = 0;
            int valorMedico = 0, valorEnfermagem = 0, valorEnfermeiros = 0, valorTerapia = 0;
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

                    } else{
                        valorMedico = Convert.ToInt32(medicos.InnerText);
                    }

                    XmlNode pessoalDeEnfermagem = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/PessoalDeEnfermagem");

                    if (pessoalDeEnfermagem == null)
                    {
                        valorEnfermagem = 0;
                    }else{
                        valorEnfermagem = Convert.ToInt32(pessoalDeEnfermagem.InnerText);
                    }

                    XmlNode enfermeiros = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Enfermeiros");

                    if (enfermeiros == null)
                    {

                        valorEnfermeiros = 0;
                    }else{
                        valorEnfermeiros = Convert.ToInt32(enfermeiros.InnerText);
                    }

                    XmlNode tecnicosDiagnosticoTerapeutica = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/TecnicosDiagnosticoTerapeutica");

                    if (tecnicosDiagnosticoTerapeutica == null)
                    {
                        valorTerapia = 0;
                    }else{
                        valorTerapia = Convert.ToInt32(tecnicosDiagnosticoTerapeutica.InnerText);
                    }


                    valor = valorMedico + valorEnfermagem + valorEnfermeiros + valorTerapia;

                    Funcionario func = new Funcionario(i, valor);
                    funcionarios.Add(func);


                }


                
                

            }




            
            return funcionarios;


        }




        
        public List<Acao> GetNumAcoesCategoria(int dataInicio, int dataFim, string categoria, string token)
        {
            double valor = 0.0;
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
                            }else{
                                valor = Convert.ToDouble(consultas.InnerText);
                            }

                            
                            Acao acao = new Acao(i, valor);
                            acoes.Add(acao);
                            
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
                                valor = Convert.ToDouble(internamentos.InnerText);
                            }

                            Acao acao = new Acao(i, valor);
                            acoes.Add(acao);

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
                                valor = Convert.ToDouble(urgencias.InnerText);
                            }

                            Acao acao = new Acao(i, valor);
                            acoes.Add(acao);

                        }
                    }

                    break;

               

                default:
                    throw new ArgumentNullException("Erro");





            }
            return acoes;


        }




        public List<Acao> GetNumAcoes(int dataInicio, int dataFim, string token)
        {

            double valorC = 0;
            double valorI = 0;
            double valorU = 0;

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


                    valorC = Convert.ToDouble(consultas.InnerText);
                    valorI = Convert.ToDouble(internamentos.InnerText);
                    valorU = Convert.ToDouble(urgencias.InnerText);




                    Acao acao = new Acao(i, valorC, valorI, valorU);
                    acoes.Add(acao);



                }

            }





            return acoes;


        }







        public List<Funcionario> GetNumFuncCategoria(int dataInicio, int dataFim, string token)
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


        }



        
        public List<Funcionario> GetNumFuncCategoriaS(int dataInicio, int dataFim, string categoria, string token)
        {
            int valor = 0;
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
                                valor = Convert.ToInt32(medicos.InnerText);
                            }

                            Funcionario func = new Funcionario(i, valor);
                            funcionarios.Add(func);
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
                                valor = Convert.ToInt32(pessoalDeEnfermagem.InnerText);
                            }

                            Funcionario func = new Funcionario(i, valor);
                            funcionarios.Add(func);
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
                                valor = Convert.ToInt32(enfermeiros.InnerText);
                            }

                            Funcionario func = new Funcionario(i, valor);
                            funcionarios.Add(func);
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
                                valor = Convert.ToInt32(tecnicos.InnerText);
                            }
                            Funcionario func = new Funcionario(i, valor);
                            funcionarios.Add(func);
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
            double valor = 0.0, valorMedicos = 0.0, valorEnfermagem = 0.0, valorEnfermeiros = 0.0, valorTerapeutica = 0.0, valorPessoal = 0.0;
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
                        valorTerapeutica = 0;
                    }
                    else
                    {
                        valorTerapeutica = Convert.ToDouble(tecnicos.InnerText);
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


                    funcionariosTotal = valorMedicos + valorEnfermagem + valorEnfermeiros + valorTerapeutica;

                    valor = despesaPessoal / funcionariosTotal;

                    Funcionario func = new Funcionario(i, valor);
                    funcionarios.Add(func);



                    


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



                    valor = despesaPessoal / despesaTotal;

                    valorPercentagem = valor * 100;


                    Funcionario func = new Funcionario(i, valorPercentagem);
                    funcionarios.Add(func);






                }

            }





            return funcionarios;


        }


        
        public List<Medicamento> GetPercentagemMedicamentos(int dataInicio, int dataFim, string token)
        {
            double valor = 0.0;
            double valorMedicamentosSns = 0.0;
            double valorMedicamentosUtente = 0.0;
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
                        valorMedicamentosSns = 0;
                    }
                    else
                    {
                        valorMedicamentosSns = Convert.ToDouble(medicamentosSns.InnerText);
                    }

                    XmlNode medicamentosUtente = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EncargosComMedicamentos/DoUtente");

                    if (medicamentosUtente == null)
                    {
                        valorMedicamentosUtente = 0;
                    }
                    else
                    {
                        valorMedicamentosUtente = Convert.ToDouble(medicamentosUtente.InnerText);
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


                    despesaMedicamentos = valorMedicamentosSns + valorMedicamentosUtente;

                    valor = despesaMedicamentos / despesaTotal;

                    valorPercentagem = valor * 100;


                    Medicamento medicamento = new Medicamento(i, valorPercentagem);
                    medicamentos.Add(medicamento);






                }

            }





            return medicamentos;


        }


        
        public List<Cama> GetMediaCamas(int dataInicio, int dataFim, string token)
        {

            double hospitais = 0.0, valorHospitaisEspecializados = 0.0, valorHospitaisGerais = 0.0;
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
                        valorHospitaisGerais = 0;
                    }
                    else
                    {
                        valorHospitaisGerais = Convert.ToDouble(hospitaisGerais.InnerText);
                    }

                    XmlNode hospitaisEspecializados = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Lotacao/HospitaisEspecialiazados");

                    if (hospitaisEspecializados == null)
                    {
                        valorHospitaisEspecializados = 0;
                    }
                    else
                    {
                        valorHospitaisEspecializados = Convert.ToDouble(hospitaisEspecializados.InnerText);
                    }





                    hospitais = valorHospitaisGerais + valorHospitaisEspecializados;


                    media = hospitais / 2;


                    Cama cama = new Cama(i, media);
                    camas.Add(cama);
                   






                }

            }





            return camas;


        }



        
        public List<Funcionario> GetRacioFuncionariosEstabelecimentos(int dataInicio, int dataFim, string token)
        {
            double valor = 0.0;
            double funcionarios = 0.0;
            double estabelecimentos = 0.0;
            double valorPercentagem = 0.0;


            checkAuthentication(token, false);
            XmlDocument doc = new XmlDocument();
            doc.Load(FILEPATH);

            List<Funcionario> listaFuncionarios = new List<Funcionario>();





            foreach (XmlNode item in doc.SelectNodes("/Projeto"))
            {
                for (int i = dataInicio; i <= dataFim; i++)
                {



                    XmlNode medicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Medicos");
                    XmlNode pessoalEnfermagem = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/PessoalDeEnfermagem");
                    XmlNode enfermeiros = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/Enfermeiros");
                    XmlNode tecnicos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/PessoalAoServico/TecnicosDiagnosticoTerapeutica");


                    XmlNode hospitaisGerais = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EstabelecimentosSaude/HospitaisGerais");
                    XmlNode hospitaisEspecializados = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EstabelecimentosSaude/HospitaisEspecialiazados");
                    XmlNode centrosSaude = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EstabelecimentosSaude/CentrosDeSaude");
                    XmlNode extensoes = doc.SelectSingleNode("//Anos[@ano=" + i + "]/EstabelecimentosSaude/ExtensoesCentroSaude");

                    funcionarios = Convert.ToDouble(medicos.InnerText) + Convert.ToDouble(pessoalEnfermagem.InnerText) + Convert.ToDouble(enfermeiros.InnerText) + Convert.ToDouble(tecnicos.InnerText);

                    estabelecimentos = Convert.ToDouble(hospitaisGerais.InnerText) + Convert.ToDouble(hospitaisEspecializados.InnerText) + Convert.ToDouble(centrosSaude.InnerText) + Convert.ToDouble(extensoes.InnerText);




                    valor = funcionarios / estabelecimentos;

                    valorPercentagem = valor * 100;

                    Funcionario func = new Funcionario(i, valorPercentagem);
                    listaFuncionarios.Add(func);




                }

            }





            return listaFuncionarios;


        }


       


        public List<Acao> GetPercentagemAcoes(int dataInicio, int dataFim, string categoria, string token)
        {
            double valor = 0.0;
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
                            XmlNode consultasTotal = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Consultas/Total");

                            valor = Convert.ToDouble(consultas.InnerText) / Convert.ToDouble(consultasTotal.InnerText);
                            percentagem = valor * 100;
                            Acao acao = new Acao(i, percentagem);
                            acoes.Add(acao);
                        }
                    }

                    break;


                case "Internamentos":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode internamentos = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Internamentos/CentrosSaude");
                            XmlNode internamentosTotal = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Internamentos/Total");

                            valor = Convert.ToDouble(internamentos.InnerText) / Convert.ToDouble(internamentosTotal.InnerText);
                            percentagem = valor * 100;
                            Acao acao = new Acao(i, percentagem);
                            acoes.Add(acao);
                        }
                    }

                    break;


                case "Urgencias":

                    foreach (XmlNode item in doc.SelectNodes("/Projeto"))
                    {
                        for (int i = dataInicio; i <= dataFim; i++)
                        {
                            XmlNode urgencias = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Urgencias/CentrosSaude");
                            XmlNode urgenciasTotal = doc.SelectSingleNode("//Anos[@ano=" + i + "]/Urgencias/Total");

                            valor = Convert.ToDouble(urgencias.InnerText) / Convert.ToDouble(urgenciasTotal.InnerText);
                            percentagem = valor * 100;
                            Acao acao = new Acao(i, percentagem);
                            acoes.Add(acao);
                        }
                    }

                    break;

                

                default:
                    throw new ArgumentNullException("Erro");





            }
            return acoes;


        }










        public void ReceberXml(string xml)
        {
            FILEPATH = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "XmlTestexml.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
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
