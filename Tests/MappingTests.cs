﻿using AutoMapper;
using Weav_App.DTOs.Entities.User;
using Weav_App.Helpers;
using Xunit;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = configuration.CreateMapper();
    }

    [Fact]
    public void RegisterUserDto_To_UserDbTable_Mapping_IsValid()
    {
        // Arrange
        var registerUserDto = new RegisterUserDto
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "Password123",
            UserIp = "127.0.0.1",
            RegisterDate = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow,
            UserPhotoUrl = "/PATH/TO/IMAGE"
        };

        // Act
        var userDbTable = _mapper.Map<UserDbTable>(registerUserDto);

        // Assert
        Assert.NotNull(userDbTable);
        Assert.Equal(registerUserDto.Username, userDbTable.Username);
        Assert.Equal(registerUserDto.Email, userDbTable.Email);
        Assert.NotEmpty(userDbTable.PasswordHash); 
    }
}