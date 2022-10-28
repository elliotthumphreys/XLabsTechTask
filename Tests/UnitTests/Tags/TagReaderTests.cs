using Application.Exceptions;
using Application.Reviews;
using Application.Tags;
using Domain;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using UnitTests.Infrastructure;

namespace UnitTests.Tags
{
    [TestFixture]
    internal class TagReaderTests : BaseTestFixture
    {
        [Test]
        public void GetById_ReturnsTagDto()
        {
            // arrange
            const int TAG_ID = 1;

            TagDto? tagDto = null;

            var tagReader = _serviceProvider.GetRequiredService<IReadTag>();

            var tag = new Tag("tag name test");

            _context.Add(tag);

            _context.SaveChanges();
           
            // act & assert
            Assert.DoesNotThrow(() => tagDto = tagReader.GetById(TAG_ID));
            Assert.IsNotNull(tagDto);

            EnsureTagMappedCorrectly(tag, tagDto!);
        }

        [Test]
        public void GetById_NoTagForId_NotFoundExceptionThrown()
        {
            // arrange
            const int TAG_ID = 1;

            var tagReader = _serviceProvider.GetRequiredService<IReadTag>();

            // act & assert
            Assert.Throws(typeof(NotFoundException), () => tagReader.GetById(TAG_ID));
        }
        
        [Test]
        public void Get_ReadAllTags()
        {
            // arrange
            var tagReader = _serviceProvider.GetRequiredService<IReadTag>();

            var tag1 = new Tag("tag 1");
            var tag2 = new Tag("tag 2");

            _context.Add(tag1);
            _context.Add(tag2);

            _context.SaveChanges();

            // act
            var tags = tagReader.Get();

            // assert
            Assert.AreEqual(2, tags.Count);

            EnsureTagMappedCorrectly(tag1, tags.First());
            EnsureTagMappedCorrectly(tag2, tags.Last());
        }
        
        private void EnsureTagMappedCorrectly(Tag tag, TagDto dto)
        {
            Assert.AreEqual(tag.Name, dto.Name);
        }
    }
}
