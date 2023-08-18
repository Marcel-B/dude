// using Wolverine.Attributes;
//
// namespace Identity.Servus.AppUser.Adapter;
//
// public class CreateUserCommand
// {
//   public int Id { get; }
//
//   public CreateUserCommand(int id)
//   {
//     Id = id;
//   }
// }
//
// [Transactional]
// public record UserCreated(int id);
//
// public class CreateUserHandler
// {
//   public static UserCreated Handle(CreateUserCommand command)
//   {
//     return new UserCreated(command.Id);
//   }
// }


