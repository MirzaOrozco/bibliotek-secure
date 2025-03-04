﻿using Infrastructure.Data;
using Application.Commands.Authors;
using Application.DataTransferObjects.Authors;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repository;
using Domain;
using Tests.MemoryDatabase;

namespace Tests.AuthorTests.CommandTest.Create
{
    public class CreateAuthorTests
    {
        private CreateAuthorCommandHandler _handler;
        private RealDatabase _db;

        public CreateAuthorTests()
        {
             _db = CreateTestDb.CreateInMemoryTestDbWithData();
            _handler = new CreateAuthorCommandHandler(new AuthorRepository(_db));
        }

        [Fact]
        public async Task ValidName_AddsAuthor()
        {
            // Arrange
            AuthorDto newAuthor = new AuthorDto
            {
                Name = "ATest"
            };
            var command = new CreateAuthorCommand(newAuthor);
            int oldCount = _db.Authors.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(operationResult.IsSuccessful);
            var result = operationResult.Data;
            Assert.NotNull(result);
            Assert.Equal(newAuthor.Name, result.Name);
            Assert.Equal(oldCount + 1, _db.Authors.Count());
            // Verify that Author was created
            Assert.Contains(_db.Authors, a => a.Name == newAuthor.Name);
        }

        [Fact]
        public async Task EmptyName_Failure()
        {
            // Arrange
            AuthorDto newAuthor = new AuthorDto
            {
                Name = String.Empty
            };
            var command = new CreateAuthorCommand(newAuthor);
            int oldCount = _db.Authors.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.Equal(oldCount, _db.Authors.Count());
        }

        [Fact]
        public async Task NameWithSpaces_Failure()
        {
            // Arrange
            AuthorDto newAuthor = new AuthorDto
            {
                Name = "    "
            };
            var command = new CreateAuthorCommand(newAuthor);
            int oldCount = _db.Authors.Count();

            // Act
            var operationResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(operationResult.IsSuccessful);
            Assert.Equal(oldCount, _db.Authors.Count());
        }
    }
}
