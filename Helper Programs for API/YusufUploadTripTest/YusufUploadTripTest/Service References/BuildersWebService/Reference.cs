﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YusufUploadTripTest.BuildersWebService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Trip", Namespace="http://wwww.trackmatic.co.za/schemas")]
    [System.SerializableAttribute()]
    public partial class Trip : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreatedOnField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime StartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private YusufUploadTripTest.BuildersWebService.Stop[] StopsField;
        
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
        public System.DateTime CreatedOn {
            get {
                return this.CreatedOnField;
            }
            set {
                if ((this.CreatedOnField.Equals(value) != true)) {
                    this.CreatedOnField = value;
                    this.RaisePropertyChanged("CreatedOn");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Location {
            get {
                return this.LocationField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationField, value) != true)) {
                    this.LocationField = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Start {
            get {
                return this.StartField;
            }
            set {
                if ((this.StartField.Equals(value) != true)) {
                    this.StartField = value;
                    this.RaisePropertyChanged("Start");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public YusufUploadTripTest.BuildersWebService.Stop[] Stops {
            get {
                return this.StopsField;
            }
            set {
                if ((object.ReferenceEquals(this.StopsField, value) != true)) {
                    this.StopsField = value;
                    this.RaisePropertyChanged("Stops");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Stop", Namespace="http://wwww.trackmatic.co.za/schemas")]
    [System.SerializableAttribute()]
    public partial class Stop : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ArrivalField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClusterNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DepartureField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private YusufUploadTripTest.BuildersWebService.DropPoint[] DropPointsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int OrderField;
        
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
        public System.DateTime Arrival {
            get {
                return this.ArrivalField;
            }
            set {
                if ((this.ArrivalField.Equals(value) != true)) {
                    this.ArrivalField = value;
                    this.RaisePropertyChanged("Arrival");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClusterName {
            get {
                return this.ClusterNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ClusterNameField, value) != true)) {
                    this.ClusterNameField = value;
                    this.RaisePropertyChanged("ClusterName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Departure {
            get {
                return this.DepartureField;
            }
            set {
                if ((this.DepartureField.Equals(value) != true)) {
                    this.DepartureField = value;
                    this.RaisePropertyChanged("Departure");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public YusufUploadTripTest.BuildersWebService.DropPoint[] DropPoints {
            get {
                return this.DropPointsField;
            }
            set {
                if ((object.ReferenceEquals(this.DropPointsField, value) != true)) {
                    this.DropPointsField = value;
                    this.RaisePropertyChanged("DropPoints");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Order {
            get {
                return this.OrderField;
            }
            set {
                if ((this.OrderField.Equals(value) != true)) {
                    this.OrderField = value;
                    this.RaisePropertyChanged("Order");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DropPoint", Namespace="http://wwww.trackmatic.co.za/schemas")]
    [System.SerializableAttribute()]
    public partial class DropPoint : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LatitudeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LongitudeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private YusufUploadTripTest.BuildersWebService.Waybill[] WaybillsField;
        
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
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Latitude {
            get {
                return this.LatitudeField;
            }
            set {
                if ((this.LatitudeField.Equals(value) != true)) {
                    this.LatitudeField = value;
                    this.RaisePropertyChanged("Latitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Longitude {
            get {
                return this.LongitudeField;
            }
            set {
                if ((this.LongitudeField.Equals(value) != true)) {
                    this.LongitudeField = value;
                    this.RaisePropertyChanged("Longitude");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public YusufUploadTripTest.BuildersWebService.Waybill[] Waybills {
            get {
                return this.WaybillsField;
            }
            set {
                if ((object.ReferenceEquals(this.WaybillsField, value) != true)) {
                    this.WaybillsField = value;
                    this.RaisePropertyChanged("Waybills");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Waybill", Namespace="http://wwww.trackmatic.co.za/schemas")]
    [System.SerializableAttribute()]
    public partial class Waybill : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private YusufUploadTripTest.BuildersWebService.Address AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double CubesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DpCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double MassField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ParcelsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReferenceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServiceTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StoreNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueRefField;
        
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
        public YusufUploadTripTest.BuildersWebService.Address Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Cubes {
            get {
                return this.CubesField;
            }
            set {
                if ((this.CubesField.Equals(value) != true)) {
                    this.CubesField = value;
                    this.RaisePropertyChanged("Cubes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomerCode {
            get {
                return this.CustomerCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerCodeField, value) != true)) {
                    this.CustomerCodeField = value;
                    this.RaisePropertyChanged("CustomerCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CustomerName {
            get {
                return this.CustomerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomerNameField, value) != true)) {
                    this.CustomerNameField = value;
                    this.RaisePropertyChanged("CustomerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DpCode {
            get {
                return this.DpCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.DpCodeField, value) != true)) {
                    this.DpCodeField = value;
                    this.RaisePropertyChanged("DpCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Mass {
            get {
                return this.MassField;
            }
            set {
                if ((this.MassField.Equals(value) != true)) {
                    this.MassField = value;
                    this.RaisePropertyChanged("Mass");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Parcels {
            get {
                return this.ParcelsField;
            }
            set {
                if ((this.ParcelsField.Equals(value) != true)) {
                    this.ParcelsField = value;
                    this.RaisePropertyChanged("Parcels");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Reference {
            get {
                return this.ReferenceField;
            }
            set {
                if ((object.ReferenceEquals(this.ReferenceField, value) != true)) {
                    this.ReferenceField = value;
                    this.RaisePropertyChanged("Reference");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServiceType {
            get {
                return this.ServiceTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceTypeField, value) != true)) {
                    this.ServiceTypeField = value;
                    this.RaisePropertyChanged("ServiceType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StoreName {
            get {
                return this.StoreNameField;
            }
            set {
                if ((object.ReferenceEquals(this.StoreNameField, value) != true)) {
                    this.StoreNameField = value;
                    this.RaisePropertyChanged("StoreName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ValueRef {
            get {
                return this.ValueRefField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueRefField, value) != true)) {
                    this.ValueRefField = value;
                    this.RaisePropertyChanged("ValueRef");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Address", Namespace="http://wwww.trackmatic.co.za/schemas")]
    [System.SerializableAttribute()]
    public partial class Address : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Line1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Line2Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Line3Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Line4Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostalCodeField;
        
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
        public string Line1 {
            get {
                return this.Line1Field;
            }
            set {
                if ((object.ReferenceEquals(this.Line1Field, value) != true)) {
                    this.Line1Field = value;
                    this.RaisePropertyChanged("Line1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Line2 {
            get {
                return this.Line2Field;
            }
            set {
                if ((object.ReferenceEquals(this.Line2Field, value) != true)) {
                    this.Line2Field = value;
                    this.RaisePropertyChanged("Line2");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Line3 {
            get {
                return this.Line3Field;
            }
            set {
                if ((object.ReferenceEquals(this.Line3Field, value) != true)) {
                    this.Line3Field = value;
                    this.RaisePropertyChanged("Line3");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Line4 {
            get {
                return this.Line4Field;
            }
            set {
                if ((object.ReferenceEquals(this.Line4Field, value) != true)) {
                    this.Line4Field = value;
                    this.RaisePropertyChanged("Line4");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostalCode {
            get {
                return this.PostalCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PostalCodeField, value) != true)) {
                    this.PostalCodeField = value;
                    this.RaisePropertyChanged("PostalCode");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://wwww.trackmatic.co.za/schemas", ConfigurationName="BuildersWebService.IIntegrationBuilders")]
    public interface IIntegrationBuilders {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://wwww.trackmatic.co.za/schemas/IIntegrationBuilders/UploadTrip", ReplyAction="http://wwww.trackmatic.co.za/schemas/IIntegrationBuilders/UploadTripResponse")]
        void UploadTrip(YusufUploadTripTest.BuildersWebService.Trip trip);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://wwww.trackmatic.co.za/schemas/IIntegrationBuilders/UploadTrip", ReplyAction="http://wwww.trackmatic.co.za/schemas/IIntegrationBuilders/UploadTripResponse")]
        System.Threading.Tasks.Task UploadTripAsync(YusufUploadTripTest.BuildersWebService.Trip trip);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IIntegrationBuildersChannel : YusufUploadTripTest.BuildersWebService.IIntegrationBuilders, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IntegrationBuildersClient : System.ServiceModel.ClientBase<YusufUploadTripTest.BuildersWebService.IIntegrationBuilders>, YusufUploadTripTest.BuildersWebService.IIntegrationBuilders {
        
        public IntegrationBuildersClient() {
        }
        
        public IntegrationBuildersClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IntegrationBuildersClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IntegrationBuildersClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IntegrationBuildersClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void UploadTrip(YusufUploadTripTest.BuildersWebService.Trip trip) {
            base.Channel.UploadTrip(trip);
        }
        
        public System.Threading.Tasks.Task UploadTripAsync(YusufUploadTripTest.BuildersWebService.Trip trip) {
            return base.Channel.UploadTripAsync(trip);
        }
    }
}