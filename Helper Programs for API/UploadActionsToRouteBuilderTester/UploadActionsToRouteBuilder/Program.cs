using Trackmatic.Rest.Planning.Model;
using UploadActionsToRouteBuilder.Transformer;

namespace UploadActionsToRouteBuilder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var profileData = KolokProfileData();
            var actions = CreateActionItem();
            var routeBuilderTransformer = new RouteBuilderTransformer(profileData, actions);
            var planActions = routeBuilderTransformer.Actions();
            //Preferable to use the pipeline isnstead of the below to push data
            var api = profileData.CreateLogin();
            var upload = api.ExecuteRequest(new Trackmatic.Rest.Planning.Requests.UploadActions(api.Context,new ActionCollection(planActions)));
        }

        //Getting Kolok's Profile instance that exists in trackmatic
        public static TrackmaticProfileData KolokProfileData()
        {
            return new TrackmaticProfileData(){};
        }

        //Creating an action item (Invoice from Kolok)
        public static ActionItem CreateActionItem()
        {
            return new ActionItem()
            {
                ActionReference = "SI031332903", //InvoiceId
                Weight = 0.0, //Mass
                Pieces = 1, // Eaches
                Instructions = "", //DocumentHandlingInstructions
                CustomerReference = "11K0217", //
                InternalReference = "", 
                ActionTypeId = "COURIER|COURIER", //ModeOfDelivery
                ActionTypeName = "COURIER|COURIER", //ModeOfDelivery
                ExpectedDelivery = new System.DateTime(2017, 11, 02),//ExpectedDelivery
                AmountIncl = 1167.0, //AmountIncl
                AmountEx = 1024.0, //AmountExcl
                Direction = EActionDirection.Outbound, //0 = Outbound; 1 = Inbound
                Measure = EUnitOfMeasure.Box, //0 = Box; 1 = Parcel; 2 = Carton
                ShipToName = "KDC PLANT HIRE CC",//InvoiceAccountName
                ShipToReference = "16997",//InvoiceAccount
                ShipToAddressId = "5637230951", //AddressRecId
                ShipToAddressName = "KDC PLANT HIRE CC", //AddressName
                UnitNo = null, //UnitNo
                BuildingName = "", //BuildingName
                StreetNo = "", //StreetNo
                SubDivisionNumber = null, //SubDivisionNumber
                Street = "27 PALM AVENUE", //Street
                Suburb = "", //Suburb
                City = "PHALABORWA", //City
                Province = "", //Province
                PostalCode = "1390" //PostalCode
            };
        }
    }
}
