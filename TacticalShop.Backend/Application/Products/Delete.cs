using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TacticalShop.Backend.Data;

namespace TacticalShop.Backend.Application.Products
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await _context.Products.FindAsync(request.id);
                _context.Remove(product);
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}