namespace Comp486GroupProject;

public class Supplier
{
    public int Id{ get; set; }
    public string Name{ get; set; }
    public string Phone{ get; set; }
    public string Email{ get; set; }

    public Supplier(int id, string name, string phone, string email)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
    }
}