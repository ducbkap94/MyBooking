namespace MyWeb.Business.Dtos.Customer.Response
{
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? IdentityCard { get; set; }
    }
}