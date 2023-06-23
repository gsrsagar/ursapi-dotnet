namespace LocalGuideAPI.Models.Implementaions
{
    public class RolesAuthorization 
        //AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {

        /* private readonly ILogger<LocalJobsController> _logger;
         private readonly LocalGuideContext _context;
         private static Logger logger = LogManager.GetCurrentClassLogger();

         public RolesAuthorization(LocalGuideContext context)
         {            
             _context = context;
         }
         public IConfiguration GetConfig()
         {
             var Config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true);
             return Config.Build();
         }
         protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        RolesAuthorizationRequirement requirement)
         {           
             if (context.User == null || !context.User.Identity.IsAuthenticated)
             {
                 context.Fail();
                 return Task.CompletedTask;
             }
             var validRole = false;
             if (requirement.AllowedRoles == null ||
                 requirement.AllowedRoles.Any() == false)
             {
                 validRole = true;
             }
             else
             {                                
                 var roles = requirement.AllowedRoles.ToList();
                 string userName = context.User.Identity.Name.Substring(6);
                 MappedDiagnosticsContext.Set("UserName", userName);
                 //string userName = context.User.Identity.Name.Substring(7);
                // logger.Info("Received UserLanID: " + userName);
                 foreach(string Item in roles)
                 {
                     if(Authentication(Item, userName))
                     {
                         validRole = true;
                         break;
                     }
                 }                                
             }

             if (validRole)
             {
                 context.Succeed(requirement);
             }
             else
             {                
                 context.Fail();                
             }
             return Task.CompletedTask;
         }


         public bool Authentication(string Role, string User)
         {
             bool Authorize = false;      

             try
             {
                 logger.Info("Checking for Authorization for User: " + User);

                 var Config = GetConfig();
                 var Form = Config.GetSection("AppSettings").GetSection("FormName").Value;
                 Form FormName = _context.Form.Where(x => x.Name.ToLower().Trim() == Form.ToLower().Trim()).AsNoTracking().SingleOrDefault();

                 if(FormName!= null)
                 {
                     FormRole UserRoleName = _context.FormRole.Where(x => x.Name.ToLower().Trim() == Role.ToLower().Trim()).AsNoTracking().SingleOrDefault();

                     if(UserRoleName != null)
                     {
                         UserFormRoles UserCount = _context.UserFormRoles.Where(x => x.UserLogin.ToLower() == User.ToLower() && x.FormId == FormName.Id && x.FormRoleId == UserRoleName.Id && x.Active == true).AsNoTracking().SingleOrDefault();
                         if (UserCount != null)
                         {
                             Authorize = true;
                         }
                     }
                     else
                     {
                         throw new Exception("Role Name does not exist. Please add the role 'Admin Interface User' in relevant table.");
                     }                    
                 }
                 else
                 {
                     throw new Exception("Form Name does not exist. Please add the form name in relevant table.");
                 }
             }
             catch (Exception Ex)
             {
                 logger.Error(Ex, Ex.Message);
                 throw new Exception();
             }
             return Authorize;

         }
     }*/
    }
}
