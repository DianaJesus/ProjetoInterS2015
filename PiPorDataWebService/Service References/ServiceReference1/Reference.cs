﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PiPorDataWebService.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Utilizador", Namespace="http://schemas.datacontract.org/2004/07/PiPorDataWebService")]
    [System.SerializableAttribute()]
    public partial class Utilizador : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool AdminField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Admin {
            get {
                return this.AdminField;
            }
            set {
                if ((this.AdminField.Equals(value) != true)) {
                    this.AdminField = value;
                    this.RaisePropertyChanged("Admin");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Funcionario", Namespace="http://schemas.datacontract.org/2004/07/PiPorDataWebService")]
    [System.SerializableAttribute()]
    public partial class Funcionario : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AnoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double Soma1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ValorField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Ano {
            get {
                return this.AnoField;
            }
            set {
                if ((this.AnoField.Equals(value) != true)) {
                    this.AnoField = value;
                    this.RaisePropertyChanged("Ano");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Soma1 {
            get {
                return this.Soma1Field;
            }
            set {
                if ((this.Soma1Field.Equals(value) != true)) {
                    this.Soma1Field = value;
                    this.RaisePropertyChanged("Soma1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Valor {
            get {
                return this.ValorField;
            }
            set {
                if ((this.ValorField.Equals(value) != true)) {
                    this.ValorField = value;
                    this.RaisePropertyChanged("Valor");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Medicamento", Namespace="http://schemas.datacontract.org/2004/07/PiPorDataWebService")]
    [System.SerializableAttribute()]
    public partial class Medicamento : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AnoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ValorField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Ano {
            get {
                return this.AnoField;
            }
            set {
                if ((this.AnoField.Equals(value) != true)) {
                    this.AnoField = value;
                    this.RaisePropertyChanged("Ano");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Valor {
            get {
                return this.ValorField;
            }
            set {
                if ((this.ValorField.Equals(value) != true)) {
                    this.ValorField = value;
                    this.RaisePropertyChanged("Valor");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Cama", Namespace="http://schemas.datacontract.org/2004/07/PiPorDataWebService")]
    [System.SerializableAttribute()]
    public partial class Cama : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AnoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ValorField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Ano {
            get {
                return this.AnoField;
            }
            set {
                if ((this.AnoField.Equals(value) != true)) {
                    this.AnoField = value;
                    this.RaisePropertyChanged("Ano");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Valor {
            get {
                return this.ValorField;
            }
            set {
                if ((this.ValorField.Equals(value) != true)) {
                    this.ValorField = value;
                    this.RaisePropertyChanged("Valor");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Acao", Namespace="http://schemas.datacontract.org/2004/07/PiPorDataWebService")]
    [System.SerializableAttribute()]
    public partial class Acao : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AnoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double Soma1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double ValorField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Ano {
            get {
                return this.AnoField;
            }
            set {
                if ((this.AnoField.Equals(value) != true)) {
                    this.AnoField = value;
                    this.RaisePropertyChanged("Ano");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Soma1 {
            get {
                return this.Soma1Field;
            }
            set {
                if ((this.Soma1Field.Equals(value) != true)) {
                    this.Soma1Field = value;
                    this.RaisePropertyChanged("Soma1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Valor {
            get {
                return this.ValorField;
            }
            set {
                if ((this.ValorField.Equals(value) != true)) {
                    this.ValorField = value;
                    this.RaisePropertyChanged("Valor");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SignUp", ReplyAction="http://tempuri.org/IService1/SignUpResponse")]
        void SignUp(PiPorDataWebService.ServiceReference1.Utilizador utilizador, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SignUp", ReplyAction="http://tempuri.org/IService1/SignUpResponse")]
        System.Threading.Tasks.Task SignUpAsync(PiPorDataWebService.ServiceReference1.Utilizador utilizador, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LogIn", ReplyAction="http://tempuri.org/IService1/LogInResponse")]
        string LogIn(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LogIn", ReplyAction="http://tempuri.org/IService1/LogInResponse")]
        System.Threading.Tasks.Task<string> LogInAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LogOut", ReplyAction="http://tempuri.org/IService1/LogOutResponse")]
        void LogOut(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LogOut", ReplyAction="http://tempuri.org/IService1/LogOutResponse")]
        System.Threading.Tasks.Task LogOutAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/IsAdmin", ReplyAction="http://tempuri.org/IService1/IsAdminResponse")]
        bool IsAdmin(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/IsAdmin", ReplyAction="http://tempuri.org/IService1/IsAdminResponse")]
        System.Threading.Tasks.Task<bool> IsAdminAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/IsLoggedIn", ReplyAction="http://tempuri.org/IService1/IsLoggedInResponse")]
        bool IsLoggedIn(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/IsLoggedIn", ReplyAction="http://tempuri.org/IService1/IsLoggedInResponse")]
        System.Threading.Tasks.Task<bool> IsLoggedInAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNumFuncionarioPorData", ReplyAction="http://tempuri.org/IService1/GetNumFuncionarioPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Funcionario[] GetNumFuncionarioPorData(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNumFuncionarioPorData", ReplyAction="http://tempuri.org/IService1/GetNumFuncionarioPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetNumFuncionarioPorDataAsync(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNumFuncCategoriaPorData", ReplyAction="http://tempuri.org/IService1/GetNumFuncCategoriaPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Funcionario[] GetNumFuncCategoriaPorData(int dataInicio, int dataFim, string categoria, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNumFuncCategoriaPorData", ReplyAction="http://tempuri.org/IService1/GetNumFuncCategoriaPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetNumFuncCategoriaPorDataAsync(int dataInicio, int dataFim, string categoria, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMediaFuncionarioPorData", ReplyAction="http://tempuri.org/IService1/GetMediaFuncionarioPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Funcionario[] GetMediaFuncionarioPorData(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMediaFuncionarioPorData", ReplyAction="http://tempuri.org/IService1/GetMediaFuncionarioPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetMediaFuncionarioPorDataAsync(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPercentagemPessoalPorData", ReplyAction="http://tempuri.org/IService1/GetPercentagemPessoalPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Funcionario[] GetPercentagemPessoalPorData(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPercentagemPessoalPorData", ReplyAction="http://tempuri.org/IService1/GetPercentagemPessoalPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetPercentagemPessoalPorDataAsync(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPercentagemMedicamentosPorData", ReplyAction="http://tempuri.org/IService1/GetPercentagemMedicamentosPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Medicamento[] GetPercentagemMedicamentosPorData(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPercentagemMedicamentosPorData", ReplyAction="http://tempuri.org/IService1/GetPercentagemMedicamentosPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Medicamento[]> GetPercentagemMedicamentosPorDataAsync(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMediaCamasPorData", ReplyAction="http://tempuri.org/IService1/GetMediaCamasPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Cama[] GetMediaCamasPorData(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMediaCamasPorData", ReplyAction="http://tempuri.org/IService1/GetMediaCamasPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Cama[]> GetMediaCamasPorDataAsync(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRacioFuncionariosPorData", ReplyAction="http://tempuri.org/IService1/GetRacioFuncionariosPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Funcionario[] GetRacioFuncionariosPorData(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRacioFuncionariosPorData", ReplyAction="http://tempuri.org/IService1/GetRacioFuncionariosPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetRacioFuncionariosPorDataAsync(int dataInicio, int dataFim, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAcoesCategoriaPorData", ReplyAction="http://tempuri.org/IService1/GetAcoesCategoriaPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Acao[] GetAcoesCategoriaPorData(int dataInicio, int dataFim, string categoria, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAcoesCategoriaPorData", ReplyAction="http://tempuri.org/IService1/GetAcoesCategoriaPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Acao[]> GetAcoesCategoriaPorDataAsync(int dataInicio, int dataFim, string categoria, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPercentagemAcoesPorData", ReplyAction="http://tempuri.org/IService1/GetPercentagemAcoesPorDataResponse")]
        PiPorDataWebService.ServiceReference1.Acao[] GetPercentagemAcoesPorData(int dataInicio, int dataFim, string categoria, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetPercentagemAcoesPorData", ReplyAction="http://tempuri.org/IService1/GetPercentagemAcoesPorDataResponse")]
        System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Acao[]> GetPercentagemAcoesPorDataAsync(int dataInicio, int dataFim, string categoria, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ReceberXml", ReplyAction="http://tempuri.org/IService1/ReceberXmlResponse")]
        void ReceberXml(string xml);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ReceberXml", ReplyAction="http://tempuri.org/IService1/ReceberXmlResponse")]
        System.Threading.Tasks.Task ReceberXmlAsync(string xml);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : PiPorDataWebService.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<PiPorDataWebService.ServiceReference1.IService1>, PiPorDataWebService.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SignUp(PiPorDataWebService.ServiceReference1.Utilizador utilizador, string token) {
            base.Channel.SignUp(utilizador, token);
        }
        
        public System.Threading.Tasks.Task SignUpAsync(PiPorDataWebService.ServiceReference1.Utilizador utilizador, string token) {
            return base.Channel.SignUpAsync(utilizador, token);
        }
        
        public string LogIn(string username, string password) {
            return base.Channel.LogIn(username, password);
        }
        
        public System.Threading.Tasks.Task<string> LogInAsync(string username, string password) {
            return base.Channel.LogInAsync(username, password);
        }
        
        public void LogOut(string token) {
            base.Channel.LogOut(token);
        }
        
        public System.Threading.Tasks.Task LogOutAsync(string token) {
            return base.Channel.LogOutAsync(token);
        }
        
        public bool IsAdmin(string token) {
            return base.Channel.IsAdmin(token);
        }
        
        public System.Threading.Tasks.Task<bool> IsAdminAsync(string token) {
            return base.Channel.IsAdminAsync(token);
        }
        
        public bool IsLoggedIn(string token) {
            return base.Channel.IsLoggedIn(token);
        }
        
        public System.Threading.Tasks.Task<bool> IsLoggedInAsync(string token) {
            return base.Channel.IsLoggedInAsync(token);
        }
        
        public PiPorDataWebService.ServiceReference1.Funcionario[] GetNumFuncionarioPorData(int dataInicio, int dataFim, string token) {
            return base.Channel.GetNumFuncionarioPorData(dataInicio, dataFim, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetNumFuncionarioPorDataAsync(int dataInicio, int dataFim, string token) {
            return base.Channel.GetNumFuncionarioPorDataAsync(dataInicio, dataFim, token);
        }
        
        public PiPorDataWebService.ServiceReference1.Funcionario[] GetNumFuncCategoriaPorData(int dataInicio, int dataFim, string categoria, string token) {
            return base.Channel.GetNumFuncCategoriaPorData(dataInicio, dataFim, categoria, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetNumFuncCategoriaPorDataAsync(int dataInicio, int dataFim, string categoria, string token) {
            return base.Channel.GetNumFuncCategoriaPorDataAsync(dataInicio, dataFim, categoria, token);
        }
        
        public PiPorDataWebService.ServiceReference1.Funcionario[] GetMediaFuncionarioPorData(int dataInicio, int dataFim, string token) {
            return base.Channel.GetMediaFuncionarioPorData(dataInicio, dataFim, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetMediaFuncionarioPorDataAsync(int dataInicio, int dataFim, string token) {
            return base.Channel.GetMediaFuncionarioPorDataAsync(dataInicio, dataFim, token);
        }
        
        public PiPorDataWebService.ServiceReference1.Funcionario[] GetPercentagemPessoalPorData(int dataInicio, int dataFim, string token) {
            return base.Channel.GetPercentagemPessoalPorData(dataInicio, dataFim, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetPercentagemPessoalPorDataAsync(int dataInicio, int dataFim, string token) {
            return base.Channel.GetPercentagemPessoalPorDataAsync(dataInicio, dataFim, token);
        }
        
        public PiPorDataWebService.ServiceReference1.Medicamento[] GetPercentagemMedicamentosPorData(int dataInicio, int dataFim, string token) {
            return base.Channel.GetPercentagemMedicamentosPorData(dataInicio, dataFim, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Medicamento[]> GetPercentagemMedicamentosPorDataAsync(int dataInicio, int dataFim, string token) {
            return base.Channel.GetPercentagemMedicamentosPorDataAsync(dataInicio, dataFim, token);
        }
        
        public PiPorDataWebService.ServiceReference1.Cama[] GetMediaCamasPorData(int dataInicio, int dataFim, string token) {
            return base.Channel.GetMediaCamasPorData(dataInicio, dataFim, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Cama[]> GetMediaCamasPorDataAsync(int dataInicio, int dataFim, string token) {
            return base.Channel.GetMediaCamasPorDataAsync(dataInicio, dataFim, token);
        }
        
        public PiPorDataWebService.ServiceReference1.Funcionario[] GetRacioFuncionariosPorData(int dataInicio, int dataFim, string token) {
            return base.Channel.GetRacioFuncionariosPorData(dataInicio, dataFim, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Funcionario[]> GetRacioFuncionariosPorDataAsync(int dataInicio, int dataFim, string token) {
            return base.Channel.GetRacioFuncionariosPorDataAsync(dataInicio, dataFim, token);
        }
        
        public PiPorDataWebService.ServiceReference1.Acao[] GetAcoesCategoriaPorData(int dataInicio, int dataFim, string categoria, string token) {
            return base.Channel.GetAcoesCategoriaPorData(dataInicio, dataFim, categoria, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Acao[]> GetAcoesCategoriaPorDataAsync(int dataInicio, int dataFim, string categoria, string token) {
            return base.Channel.GetAcoesCategoriaPorDataAsync(dataInicio, dataFim, categoria, token);
        }
        
        public PiPorDataWebService.ServiceReference1.Acao[] GetPercentagemAcoesPorData(int dataInicio, int dataFim, string categoria, string token) {
            return base.Channel.GetPercentagemAcoesPorData(dataInicio, dataFim, categoria, token);
        }
        
        public System.Threading.Tasks.Task<PiPorDataWebService.ServiceReference1.Acao[]> GetPercentagemAcoesPorDataAsync(int dataInicio, int dataFim, string categoria, string token) {
            return base.Channel.GetPercentagemAcoesPorDataAsync(dataInicio, dataFim, categoria, token);
        }
        
        public void ReceberXml(string xml) {
            base.Channel.ReceberXml(xml);
        }
        
        public System.Threading.Tasks.Task ReceberXmlAsync(string xml) {
            return base.Channel.ReceberXmlAsync(xml);
        }
    }
}
