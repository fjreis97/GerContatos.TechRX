﻿using AutoMapper;
using Core.Dto.Contato;
using Core.Dto.DDD;
using Core.Entities;
using Core.Interfaces.Repositories;
using GerContatos.API.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Dtos.Responses;
using Business.Interfaces.Services;
using Business.Services;

namespace Testes.Services
{
    [TestFixture]
    public class ContatoServiceTestes
    {
        private Mock<IContatoRepository> _mockContatoRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<IDDDService> _mockDDDService;
        private ContatoService _contatoService;

        [SetUp]
        public void SetUp()
        {
            _mockContatoRepository = new Mock<IContatoRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockDDDService = new Mock<IDDDService>();

            _contatoService = new ContatoService(
                _mockContatoRepository.Object,
                _mockMapper.Object,
                _mockDDDService.Object
            );
        }

        [Test]
        public async Task Create_ShouldReturnSuccess_WhenValidDataIsProvided()
        {
            // Arrange
            var contato = new Contato { DDDId = 1 };
            var dddDto = new Response<GetByIdDDDDto?> { Data = new GetByIdDDDDto() };
            var contatoDto = new CreateContatoDto();

            _mockDDDService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(dddDto);
            _mockMapper.Setup(m => m.Map<DDD>(dddDto.Data)).Returns(new DDD());
            _mockMapper.Setup(m => m.Map<CreateContatoDto>(It.IsAny<Contato>())).Returns(contatoDto);
            _mockContatoRepository.Setup(r => r.Create(It.IsAny<Contato>())).Returns(Task.CompletedTask);

            // Act
            var result = await _contatoService.Create(contato);

            // Assert
            Assert.AreEqual("Contato criado com sucesso", result.Message);
            Assert.AreEqual(contatoDto, result.Data);
        }

        [Test]
        public async Task Delete_ShouldReturnSuccess_WhenContatoExists()
        {
            // Arrange
            var contatoId = 1;
            _mockContatoRepository.Setup(r => r.GetById(contatoId)).ReturnsAsync(new Contato());
            _mockContatoRepository.Setup(r => r.Delete(contatoId)).Returns(Task.CompletedTask);

            // Act
            var result = await _contatoService.Delete(contatoId);

            // Assert
            Assert.AreEqual("Contato Excluido com Sucesso", result.Message); // Ajuste para mensagem real
        }

        [Test]
        public async Task GetAll_ShouldReturnContacts_WhenContactsExist()
        {
            // Arrange
            var contatos = new List<Contato> { new Contato(), new Contato() };
            var contatoDtos = new List<GetAllContatoDto> { new GetAllContatoDto(), new GetAllContatoDto() };

            _mockContatoRepository.Setup(r => r.GetAll()).ReturnsAsync(contatos);
            _mockMapper.Setup(m => m.Map<List<GetAllContatoDto>>(contatos)).Returns(contatoDtos);

            // Act
            var result = await _contatoService.GetAll();

            // Assert
            Assert.AreEqual(25, result.PageSize);
            Assert.AreEqual(contatoDtos, result.Data);
        }

        [Test]
        public async Task GetById_ShouldReturnContact_WhenContactExists()
        {
            // Arrange
            var contatoId = 1;
            var contato = new Contato();
            var contatoDto = new GetByIdContatoDto();

            _mockContatoRepository.Setup(r => r.GetById(contatoId)).ReturnsAsync(contato);
            _mockMapper.Setup(m => m.Map<GetByIdContatoDto>(contato)).Returns(contatoDto);

            // Act
            var result = await _contatoService.GetById(contatoId);

            // Assert
            Assert.AreEqual(contatoDto, result.Data);
        }

        [Test]
        public async Task Update_ShouldReturnSuccess_WhenValidDataIsProvided()
        {
            // Arrange
            var contato = new Contato { DDDId = 1 };
            var dddDto = new Response<GetByIdDDDDto?> { Data = new GetByIdDDDDto() };
            var contatoDto = new UpdateContatoDto();

            _mockDDDService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(dddDto);
            _mockMapper.Setup(m => m.Map<DDD>(dddDto.Data)).Returns(new DDD());
            _mockMapper.Setup(m => m.Map<UpdateContatoDto>(It.IsAny<Contato>())).Returns(contatoDto);
            _mockContatoRepository.Setup(r => r.Update(It.IsAny<Contato>())).Returns(Task.CompletedTask);

            // Act
            var result = await _contatoService.Update(contato);

            // Assert
            Assert.AreEqual("Contato criado com sucesso", result.Message); // Ajuste para mensagem real
            Assert.AreEqual(contatoDto, result.Data);
        }
    }
}