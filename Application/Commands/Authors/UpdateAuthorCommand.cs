using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Authors
{
    public class UpdateAuthorCommand
    {
        public int Id { get; set; }
        public string NewName { get; set; }
    }
}
