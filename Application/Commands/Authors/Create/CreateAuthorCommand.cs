﻿using Application.DataTransferObjects.Authors;
using Domain;
using MediatR;

namespace Application.Commands.Authors
{
    public class CreateAuthorCommand : IRequest<OperationResult<Author>>
    {
        public CreateAuthorCommand(AuthorDto newAuthor)
        {
            NewAuthor = newAuthor;
        }

        public AuthorDto NewAuthor { get; }
    }
}
