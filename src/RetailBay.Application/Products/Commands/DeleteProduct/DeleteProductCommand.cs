using MediatR;
using System;

namespace RetailBay.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<int>
    {
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
