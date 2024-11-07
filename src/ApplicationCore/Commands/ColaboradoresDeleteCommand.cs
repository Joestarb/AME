using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commands
{
    public class ColaboradoresDeleteCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public ColaboradoresDeleteCommand(int id)
        {
            Id = id;
        }
    }
}
