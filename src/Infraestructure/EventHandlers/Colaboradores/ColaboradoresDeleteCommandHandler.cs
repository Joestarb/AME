using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.EventHandlers.Colaboradores
{
    public class ColaboradoresDeleteCommandHandler : IRequestHandler<ColaboradoresDeleteCommand, bool>
    {
        private readonly IColaboradoresService _colaboradoresService;

        public ColaboradoresDeleteCommandHandler(IColaboradoresService colaboradoresService)
        {
            _colaboradoresService = colaboradoresService;
        }

        public async Task<bool> Handle(ColaboradoresDeleteCommand request, CancellationToken cancellationToken)
        {
            return await _colaboradoresService.DeleteColaboradorAsync(request.Id);
        }
    }
}
