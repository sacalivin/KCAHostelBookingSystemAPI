using DAL_CRUD.Models;

namespace kcauHosteslAdmin.Models
{
    public class HostelFormViewModel : Hostel
    {
        public IFormFile Image { get; set; }

    }
}
