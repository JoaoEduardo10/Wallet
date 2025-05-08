using Wallet.Application.Dtos.User;
using Wallet.Domain.Entities;

namespace Wallet.Application.Mapping
{
    public static class UserMapping
    {
        public static User ToEntity(CreateUserDto dto)
        {
            return new User
            {
                Email = dto.Email,
                Name = dto.Name,
                Password = dto.Password,
                CreatedAt = DateTime.UtcNow,
                Wallet = new Domain.Entities.Wallet()
            };
        }

        public static CreateUserDto ToDto(User user)
        {
            return new CreateUserDto
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            };
        }
    }
}
