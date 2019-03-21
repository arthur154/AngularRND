using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            MemberDA da = new MemberDA();
            var doc = await da.GetDoc(id);
            return Ok(doc);
        }
        [HttpGet]
        public  IActionResult Get()
        {
            MemberDA da = new MemberDA();
            var doc = da.GetDocList();
            return Ok(doc);
        }
    }

    public class MemberDA
    {
        private DocumentClient _client;
        private string _dataBaseId;
        private string _collectionId;
        private string _endpoint;
        private string _authKey;

        public MemberDA()
        {
            _endpoint = "https://egdrnd.documents.azure.com:443/";
            _authKey = "hmlzE3qX52sJH0U1UWRqOd23urjLIdKfUlDKtCFOEgOcqolddJG4Nzz93WZN19YfnqdDpLbPmzXA7mNA1vV6uw==";
            _client = new DocumentClient(new Uri(_endpoint), _authKey);
            _dataBaseId = "egdrnd";
            _collectionId = "member";
        }

        public async Task<Member> GetDoc(string id)
        {
            var result = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_dataBaseId, _collectionId, id));
            Member response = null;
            response = JsonConvert.DeserializeObject<Member>(result.Resource.ToString());
            return response;
        }
        public List<Member> GetDocList()
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 10};
            IQueryable<Member> members = _client.CreateDocumentQuery<Member>(
                   UriFactory.CreateDocumentCollectionUri(_dataBaseId, _collectionId), queryOptions);
            return members.ToList();
        }
    }
    public class Member
    {

        [JsonProperty(PropertyName = "ContactInformation")]
        public ContactInformation ContactInformation { get; set; }
        [JsonProperty(PropertyName = "PrimaryCarePhysician")]
        public Physician PrimaryCarePhysician { get; set; }
        [JsonProperty(PropertyName = "Portal")]
        public Portal Portal { get; set; }
        [JsonProperty(PropertyName = "Persona")]
        public Persona Persona { get; set; }
        [JsonProperty(PropertyName = "Preferences")]
        public List<MemberPreference> Preferences { get; set; }
        [JsonProperty(PropertyName = "PersonType")]
        public string PersonType { get; set; }
        [JsonProperty(PropertyName = "PersonMasterCode")]
        public string PersonMasterCode { get; set; }
        [JsonProperty(PropertyName = "MemberId")]
        public string MemberId { get; set; }
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "MiddleInitial")]
        public string MiddleInitial { get; set; }
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }
        [JsonProperty(PropertyName = "Age")]
        public int Age { get; set; }
        [JsonProperty(PropertyName = "Gender")]
        public string Gender { get; set; }
        [JsonProperty(PropertyName = "Relationship")]
        public string Relationship { get; set; }
        [JsonProperty(PropertyName = "Status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "OriginalEffectiveDate")]
        public DateTime? OriginalEffectiveDate { get; set; }
        [JsonProperty(PropertyName = "EffectiveStartDate")]
        public DateTime? EffectiveStartDate { get; set; }
        [JsonProperty(PropertyName = "EffectiveEndDate")]
        public string EffectiveEndDate { get; set; }
        [JsonProperty(PropertyName = "NewMember")]
        public string NewMember { get; set; }
        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "Group50")]
        public string Group50 { get; set; }
        [JsonProperty(PropertyName = "MartialStatus")]
        public string MartialStatus { get; set; }
        [JsonProperty(PropertyName = "IsCOBEligible")]
        public bool? IsCOBEligible { get; set; }
        [JsonProperty(PropertyName = "PersonGUID")]
        public string PersonGUID { get; set; }
        public string id { get { return PersonGUID; } }
        [JsonProperty(PropertyName = "SSN")]
        public string SSN { get; set; }

    }

    public class Contract
    {
        [JsonProperty(PropertyName = "ContractId")]
        public string ContractId { get; set; }
        [JsonProperty(PropertyName = "ProviderNetwork")]
        public ProviderNetwork ProviderNetwork { get; set; }
        //public PaymentHistory PaymentHistory { get; set; }
        [JsonProperty(PropertyName = "Billing")]
        public Billing Billing { get; set; }
        [JsonProperty(PropertyName = "Copay")]
        public Copay Copay { get; set; }
        [JsonProperty(PropertyName = "Precert")]
        public Precert Precert { get; set; }
        [JsonProperty(PropertyName = "PrimaryMember")]
        public Member PrimaryMember { get; set; }
        [JsonProperty(PropertyName = "LineOfBusiness")]
        public string LineOfBusiness { get; set; }
        [JsonProperty(PropertyName = "ContractStartDate")]
        public DateTime? ContractStartDate { get; set; }
        [JsonProperty(PropertyName = "ContractEndDate")]
        public DateTime? ContractEndDate { get; set; }
        [JsonProperty(PropertyName = "IsSubsidizedContract")]
        public bool? IsSubsidizedContract { get; set; }
        [JsonProperty(PropertyName = "SubsidyAmount")]
        public decimal? SubsidyAmount { get; set; }
        [JsonProperty(PropertyName = "IDCardMailedDate")]
        public DateTime? IDCardMailedDate { get; set; }
        [JsonProperty(PropertyName = "CanOrderMedicalIdCard")]
        public bool? CanOrderMedicalIdCard { get; set; }
        [JsonProperty(PropertyName = "CanOrderDentalIdCard")]
        public bool? CanOrderDentalIdCard { get; set; }
        [JsonProperty(PropertyName = "CoveredDependentCount")]
        public int? CoveredDependentCount { get; set; }
        [JsonProperty(PropertyName = "CostShareReduction")]
        public bool? CostShareReduction { get; set; }
        [JsonProperty(PropertyName = "PCPRequired")]
        public bool? PCPRequired { get; set; }
        [JsonProperty(PropertyName = "BusLevel7")]
        public string BusLevel7 { get; set; }
        [JsonProperty(PropertyName = "NumOfContracts")]
        public int? NumOfContracts { get; set; }
    }

    public class MemberPreference
    {
        [JsonProperty(PropertyName = "Type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }
        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }
        [JsonProperty(PropertyName = "Application")]
        public string Application { get; set; }
        [JsonProperty(PropertyName = "Guid")]
        public string Guid { get; set; }
        [JsonProperty(PropertyName = "PersonPreferenceGUID")]
        public string PersonPreferenceGUID { get; set; }

    }

    public class Persona
    {
        [JsonProperty(PropertyName = "PersonaSource")]
        public string PersonaSource { get; set; }
        [JsonProperty(PropertyName = "PersonaName")]
        public string PersonaName { get; set; }
        [JsonProperty(PropertyName = "PersonaCode")]
        public string PersonaCode { get; set; }
    }

    public class Portal
    {
        [JsonProperty(PropertyName = "UserId")]
        public string UserId { get; set; }
        [JsonProperty(PropertyName = "LastPortalAccessDate")]
        public string LastPortalAccessDate { get; set; }
        [JsonProperty(PropertyName = "LastPasswordChanged")]
        public string LastPasswordChanged { get; set; }
        [JsonProperty(PropertyName = "InvalidLoginAttempts")]
        public string InvalidLoginAttempts { get; set; }
        [JsonProperty(PropertyName = "UserLockedOut")]
        public bool UserLockedOut { get; set; }
        [JsonProperty(PropertyName = "IsUserAccountActive")]
        public bool IsUserAccountActive { get; set; }
        [JsonProperty(PropertyName = "HasPortalAccount")]
        public string HasPortalAccount { get; set; }
        [JsonProperty(PropertyName = "PortalAccountCreatedDate")]
        public string PortalAccountCreatedDate { get; set; }
        [JsonProperty(PropertyName = "FirstLoginDate")]
        public string FirstLoginDate { get; set; }
        [JsonProperty(PropertyName = "FirstLoginChannel")]
        public string FirstLoginChannel { get; set; }
        [JsonProperty(PropertyName = "SecurityRoleID")]
        public string SecurityRoleID { get; set; }
        [JsonProperty(PropertyName = "SecurityRoleDescription")]
        public string SecurityRoleDescription { get; set; }
    }

    public class Precert
    {
        [JsonProperty(PropertyName = "CMAssigned")]
        public string CMAssigned { get; set; }
        [JsonProperty(PropertyName = "CMPhone")]
        public string CMPhone { get; set; }
        [JsonProperty(PropertyName = "CaseType")]
        public string CaseType { get; set; }
        [JsonProperty(PropertyName = "ClosedDT")]
        public string ClosedDT { get; set; }
        [JsonProperty(PropertyName = "ID")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "MemberId")]
        public string MemberId { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "OpenedDate")]
        public string OpenedDate { get; set; }
    }

    public class MemberPrecert
    {
        [JsonProperty(PropertyName = "Precerts")]
        public List<Precert> Precerts { get; set; }
        [JsonProperty(PropertyName = "IsCareManagement")]
        public string IsCareManagement { get; set; }
    }

    public class Copay
    {
        public string PcpCopay { get; set; }
        public string SpecCopay { get; set; }
        public string UcCopayCommon { get; set; }
        public string UcCopayIn { get; set; }
        public string UcCopayOon { get; set; }
        public string UcCopayIndicator { get; set; }
        public string ErCopayCommon { get; set; }
        public string ErCopayIn { get; set; }
        public string ErCopayOon { get; set; }
        public string ErCopayIndicator { get; set; }
        public string EnrolledInAutoCopay { get; set; }
    }

    public class Billing
    {
        public string BillThruDt { get; set; }
        public string CashAmount { get; set; }
        public string DueAmount { get; set; }
        public string CashDt { get; set; }
        public string PaidToDate { get; set; }
        public string TotPremiumDue { get; set; }
        public string EnrolledInAutoPay { get; set; }
        public string PremiumRequired { get; set; }
        public string MonthlyPremiumAmount { get; set; }
        public string NextPaymentDueDate { get; set; }
    }

    public class ProviderNetwork
    {
        public string NetworkName { get; set; }
    }

    public class Physician
    {
                public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NPI { get; set; }
        public ContactInformation ContactInformation { get; set; }
        public string HasPCPCopay { get; set; }
        public string CopayAmount { get; set; }
        public string CopayFor3Visits { get; set; }
    }

    public class ContactInformation
    {
        public ContactInformation()
        {
        }

        //public List<ContactInformationEmail> Emails { get; set; }
        //public List<ContactInformationAddressDetails> Addresses { get; set; }
        //public List<ContactInformationPhones> Phones { get; set; }
    }
}
