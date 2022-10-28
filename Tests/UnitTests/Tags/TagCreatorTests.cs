using Application.Exceptions;
using Application.Tags;
using Domain;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Tags
{
    [TestFixture]
    internal class TagCreatorTests : BaseTestFixture
    {
        [Test]
        public void Create_TagCreated()
        {
            // arrange
            var createTagDto = new CreateTagDto
            {
                Name = "test venue"
            };

            var tagCreator = _serviceProvider.GetRequiredService<ICreateTag>();

            // act
            var createdTag = tagCreator.Create(createTagDto);

            // assert
            Assert.IsTrue(_context.Set<Tag>().Any(x => x.TagId == createdTag.TagId));

            EnsureTagMappedCorrectly(createTagDto, createdTag);
        }

        [Test]
        public void Create_WithIncorrectInvalidName_ThrowValidationException()
        {
            // arrange
            const int TAG_ID = 1;

            var createTagDto = new CreateTagDto
            {
                Name = string.Empty
            };

            var tagCreator = _serviceProvider.GetRequiredService<ICreateTag>();

            // act & assert
            Assert.Throws(typeof(ValidationException), () => tagCreator.Create(createTagDto));
            
            Assert.IsFalse(_context.Set<Tag>().Any(x => x.TagId == TAG_ID));
        }
        
        [Test]
        public void Create_WithAlreadyUsedName_ThrowValidationException()
        {
            // arrange
            const int TAG_ID = 2;

            const string Name = "Example tag name already used";

            var createTagDto = new CreateTagDto
            {
                Name = Name
            };

            var tagCreator = _serviceProvider.GetRequiredService<ICreateTag>();

            var existingTag = new Tag(Name);

            _context.Add(existingTag);

            _context.SaveChanges();

            // act & assert
            Assert.Throws(typeof(ValidationException), () => tagCreator.Create(createTagDto));
            
            Assert.IsFalse(_context.Set<Tag>().Any(x => x.TagId == TAG_ID));
        }

        private void EnsureTagMappedCorrectly(CreateTagDto tag, TagDto dto)
        {
            Assert.AreEqual(tag.Name, dto.Name);
        }
    }
}
