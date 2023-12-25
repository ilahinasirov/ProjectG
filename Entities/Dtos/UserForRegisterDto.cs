using Core.Entities.Dtos.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entities.Dtos
{
	public class UserForRegisterDto:IDto
	{
        public UserForRegisterDto()
        {
            
        }

        public string Name { get; set; }
		public string SurName { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public int SelectedDepartment { get; set; }

		public string Password { get; set; }

		public string RePassword { get; set; }

        public List<SelectListItem> Departments { get; set; }
    }
}
