using ApplicationCore.Wrappers;
using MediatR;

namespace ApplicationCore.Commands
{
    public class UpdateEstudianteCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
    }
}
