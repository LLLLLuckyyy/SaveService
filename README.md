# SaveService

HOW TO START:
1) Open solution (SaveService/SaveService.Auth.Api/SaveService.sln).
2) Open NPM, select SaveService.Resource.Api project.
3) Input: "update database".
4) Start SaveService.Auth.Api project. Login with log:"admin", pas:"admin" (or register as a user) and copy your token.
5) Start SaveService.Resource.Api project. Click "Authorize" button and input your token.
6) Try to save/get/edit/delete your messages/files.

ABOUT:
That is a service of saving messages/files.
Solution contains:
- Authentication Api.
- Api that has resource part (i.e part, that provides access to user's messages/files).
- Extra class library.
- Unit tests of resource part.

Used services: 
- Auto Mapper
- Swashbuckle
- Entity Framework Core
- System.IdentityModel.Tokens.Jwt
- Moq and XUnit frameworks
