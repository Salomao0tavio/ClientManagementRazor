using System.ComponentModel.DataAnnotations;

namespace B2B.Domain.Entities;

public class Customer
{
    #region Construtores

    //public Customer()
    //{
    //}

    public Customer(string name, string email, string phone, string address, string business)
    {
        ID = Guid.NewGuid();
        Name = name;
        Email = email;
        Phone = phone;
        Address = address;
        Business = business;
    }

    //public Customer(Guid id, string name, string email, string phone, string address, string business)
    //{
    //    ID = id;
    //    Name = name;
    //    Email = email;
    //    Phone = phone;
    //    Address = address;
    //    Business = business;
    //}

    #endregion

    #region Atributos

    [Key] public Guid ID { get; set; }

    [MaxLength(100)]
    [Display(Name = "Nome")]
    public string Name { get; set; }

    [MaxLength(150)]
    [Display(Name = "E-mail")]
    public string Email { get; set; }


    [Display(Name = "Telefone")] public string Phone { get; set; }

    [MaxLength(200)]
    [Display(Name = "Endereço")]
    public string Address { get; set; }

    [MaxLength(100)]
    [Display(Name = "Empresa")]
    public string Business { get; set; }

    #endregion
}