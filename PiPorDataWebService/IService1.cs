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
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/funcionarios?token={token}")]
        //SOAP
        [OperationContract(Name = "GetNumFuncionarios")]
        int GetNumFunc(string token);

        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/funcionarios/{categoria}?token={token}")]
        //SOAP
        [OperationContract(Name = "GetNumFuncionario")]
        int GetNumFunc(string categoria, string token);
        //REST
        [WebInvoke(Method = "GET", UriTemplate = "/funcionarios/dataInicio={dataInicio}&dataFim={dataFim}?token={token}")]
        //SOAP
        [OperationContract(Name = "GetNumFuncionarioPorData")]
        List<Funcionario> GetNumFunc(string dataInicio, string dataFim, string token);
       

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
        private int soma;
        private string categoria;
        private DateTime dataInicio;
        private DateTime dataFim;

        public Funcionario(string categoria, int soma)
        {
            this.categoria = categoria;
            this.soma = soma;
        }
        

        [DataMember]
        public string Categoria
        {
            get { return categoria; }
            set { categoria= value; }
        }
        [DataMember]
        public int Soma
        {
            get { return soma; }
            set { soma = value; }
        }

        [DataMember]
        public DateTime DataInicio
        {
            get { return dataInicio; }
            set { dataInicio = value; }
        }

        [DataMember]
        public DateTime DataFim
        {
            get { return dataFim; }
            set { dataFim = value; }
        }


    }
}

