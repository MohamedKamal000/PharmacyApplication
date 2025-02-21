namespace DataAccess
{
    
    public class Users
    {
        public int Id = -1;
        public string PhoneNumber { get; set; }
        
        public string UserName { get; set; }
        public string Password { get; set; } 
        public bool Role { get; set; }

        public override string ToString()
        {
            return $@"id: {Id}
                      PhoneNumber: {PhoneNumber}
                      UserName: {UserName}
                      Password: {Password}
                      Role: {Role}";
        }
    }

    public enum Orders
    {
        
    }

    public enum Products
    {
        
    }

    public enum DeliveryMan
    {
        
    }

    public enum MedicalCategory
    {
        
    }

    public enum SubMedicalCategory
    {
        
    }
}