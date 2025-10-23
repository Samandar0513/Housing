using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLayer.DTOs
{
    internal class PropertyDocumentInsertDto
    {
        [Required(ErrorMessage = "Kadastr raqami majburiy")]
        [StringLength(30, MinimumLength = 5)]
        public string CadastralNumber { get; set; }
    }
}
