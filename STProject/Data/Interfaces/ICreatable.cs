using STProject.Data.Models;


namespace STProject.Data.Interfaces
{
    public interface ICreatable
    {
        DateTime CreatedOn { get; set; }
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
