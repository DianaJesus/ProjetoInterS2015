using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PiPorDataWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    /*[ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }*/


    [ServiceContract]
    public interface IService1
    {
        // AUTHENTICATION
        //REST
        [WebInvoke(Method = "POST", UriTemplate = "/signup?token={token}")]
        //SOAP
        [OperationContract]
        void SignUp(Utilizador utilizador, string token);
        //REST
        [WebInvoke(Method = "POST", UriTemplate = "/login?username={username}&password={password}")]
        //SOAP
        [OperationContract]
        string LogIn(string username, string password);
        //REST
        [WebInvoke(Method = "POST", UriTemplate = "/logout")]
        //SOAP
        [OperationContract]
        void LogOut(string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/isadmin?token={token}")]
        //SOAP
        [OperationContract]
        bool IsAdmin(string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/isloggedin?token={token}")]
        //SOAP
        [OperationContract]
        bool IsLoggedIn(string token);

        //Metodos de pesquisa para service

        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/funcionarios?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetNumFuncionarioPorData")]
        List<Funcionario> GetNumFunc(int dataInicio, int dataFim, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/acoes?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetNumAcoesPorData")]
        List<Acao> GetNumAcoes(int dataInicio, int dataFim, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/funcionariosCategoria?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetNumFuncCatPorData")]
        List<Funcionario> GetNumFuncCategoria(int dataInicio, int dataFim, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/funcionariosCategoriaS?dataInicio={dataInicio}&dataFim={dataFim}&categoria={categoria}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetNumFuncCategoriaPorData")]
        List<Funcionario> GetNumFuncCategoriaS(int dataInicio, int dataFim, string categoria, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/funcionariosMedia?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetMediaFuncionarioPorData")]
        List<Funcionario> GetMediaFuncionario(int dataInicio, int dataFim, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/percentagemPessoal?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetPercentagemPessoalPorData")]
        List<Funcionario> GetPercentagemPessoal(int dataInicio, int dataFim, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/percentagemMedicamentos?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetPercentagemMedicamentosPorData")]
        List<Medicamento> GetPercentagemMedicamentos(int dataInicio, int dataFim, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/mediaCamas?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetMediaCamasPorData")]
        double GetMediaCamas(int dataInicio, int dataFim, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/racioFuncionarios?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetRacioFuncionariosPorData")]
        List<Funcionario> GetRacioFuncionariosEstabelecimentos(int dataInicio, int dataFim, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/acoesCategoria?dataInicio={dataInicio}&dataFim={dataFim}&categoria={categoria}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetAcoesCategoriaPorData")]
        List<Acao> GetNumAcoesCategoria(int dataInicio, int dataFim, string categoria, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/percentagemAcoes?dataInicio={dataInicio}&dataFim={dataFim}&token={token}")]
        //SOAP
        [OperationContract(Name = "GetPercentagemAcoesPorData")]
        List<Acao> GetPercentagemAcoes(int dataInicio, int dataFim, string token);




    }





    [DataContract]
    public class Utilizador
    {
        private string username;
        private string password;
        private bool admin;
        public Utilizador(string username, string password, bool admin)
        {
            this.admin = admin;
            this.username = username;
            this.password = password;
        }
        [DataMember]
        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }
        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }

        }
        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }

    [DataContract]
    public class Funcionario
    {
        private int ano;
        private int soma1;
        private int soma2;
        private int soma3;
        private double valor;


        public Funcionario(int ano, int soma1)
        {

            this.ano = ano;
            this.soma1 = soma1;
        }


        public Funcionario(int ano, int soma1, int soma2, int soma3)
        {

            this.ano = ano;
            this.soma1 = soma1;
            this.soma2 = soma2;
            this.soma3 = soma3;
        }


        public Funcionario(int ano, double valor)
        {

            this.ano = ano;
            this.valor = valor;
        }




        [DataMember]
        public int Soma1
        {
            get { return soma1; }
            set { soma1 = value; }
       }


        [DataMember]
        public int Soma2
        {
            get { return soma2; }
            set { soma2 = value; }
        }


        [DataMember]
        public int Soma3
        {
            get { return soma3; }
            set { soma3 = value; }
        }




        [DataMember]
        public int Ano
        {
            get { return ano; }
            set { ano = value; }
        }


        [DataMember]
        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }



    }



    

    [DataContract]
    public class Acao
    {
        private int ano;
        private double soma1;
        private double soma2;
        private double soma3;



        public Acao(int ano, double soma1)
        {
            this.ano = ano;
            this.soma1 = soma1;
        }


        public Acao(int ano, double soma1, double soma2, double soma3)
        {

            this.ano = ano;
            this.soma1 = soma1;
            this.soma2 = soma2;
            this.soma3 = soma3;

        }


        [DataMember]
        public double Soma1
        {
            get { return soma1; }
            set { soma1 = value; }

        }


        [DataMember]
        public double Soma2
        {
            get { return soma2; }
            set { soma2 = value; }

        }



        [DataMember]
        public double Soma3
        {
            get { return soma3; }
            set { soma3 = value; }

        }



        [DataMember]
        public int Ano
        { 
            get { return ano; }
            set { ano = value; }
        }



    }



    [DataContract]
    public class Medicamento
    {
        private int ano;
        private double valor;


        public Medicamento(int ano, double valor)
        {

            this.ano = ano;
            this.valor = valor;
        }




        [DataMember]
        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }


       


        [DataMember]
        public int Ano
        {
            get { return ano; }
            set { ano = value; }
        }


       



    }





}

