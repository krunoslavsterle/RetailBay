using MediatR;
using System;

namespace RetailBay.Application.Products.Queries.GetEditProductVM
{
    public class GetEditProductVMQuery : IRequest<EditProductVM>
    {
        public GetEditProductVMQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
