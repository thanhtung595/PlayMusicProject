using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayMusicProject.EntityData;
using PlayMusicProject.Models;

namespace PlayMusicProject.Controllers
{
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;
        private readonly AppDbContext _dbContext;
        public string _UserNameCookis;
        public string nameMusic;
        private readonly IWebHostEnvironment _enviroment;

        public MessageController(ILogger<MessageController> logger, AppDbContext dbContext, IWebHostEnvironment enviroment)
        {
            _logger = logger;
            _dbContext = dbContext;
            _enviroment = enviroment;
        }
        [Authorize]
        public IActionResult MessageHome(string searchSDTUser, int id)
        {
            int idUserChat = 0; 
            int sdtUserChat = 0;
            List<User> listUsers = new List<User>();
            List<User> listUserReceive = new List<User>();
            List<User> checkFormart = new List<User>();
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }
            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                     SDTUser = us.SDTUser,
                                 };
                foreach (var us in AcountUser)
                {
                    idUserChat = us.IdUser;
                    sdtUserChat = us.SDTUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }

            if (id > 0)
            {
                var checkAddResr = from u in _dbContext.UserEntity
                                              join usr in _dbContext.MyFriendUserEntity
                                              on u.IdUser equals usr.IdUserReceive
                                              where u.IdUser == id 
                                              select new User()
                                              {
                                                  IdUser = u.IdUser,
                                                  UserName = u.UserName,
                                                  UserImage = u.UserImage,
                                                  IsMyFriendUser = usr.IsMyFriendUser,
                                              };
                bool isMyFriend = false;
                foreach (var item in checkAddResr.ToList())
                {
                    if (item.IdUser == id)
                    {
                        isMyFriend = true;
                        break;
                    }
                }
                if (isMyFriend == false)
                {
                    var add = new pmoMyFriendUserEntity
                    {
                        IdUserSend = idUserChat,
                        IdUserReceive = id,
                        IsMyFriendUser = false
                    };
                    _dbContext.MyFriendUserEntity.Add(add);
                    _dbContext.SaveChanges();
                    var add2 = new pmoMyFriendUserEntity
                    {
                        IdUserSend = id,
                        IdUserReceive = idUserChat,
                        IsMyFriendUser = false
                    };
                    _dbContext.MyFriendUserEntity.Add(add2);
                    _dbContext.SaveChanges();
                }
            }

            var showMyFriend = from us in _dbContext.MyFriendUserEntity
                               join urs in _dbContext.UserEntity
                               on us.IdUserReceive equals urs.IdUser
                               where us.IdUserSend == idUserChat
                               select new User()
                               {
                                   IdUser = urs.IdUser,
                                   UserName = urs.UserName,
                                   UserImage = urs.UserImage,
                                   IsMyFriendUser = us.IsMyFriendUser,
                               };
            listUsers = showMyFriend.ToList();
            var people = from u in _dbContext.UserEntity
                             where (string.IsNullOrEmpty(searchSDTUser) || u.SDTUser.ToString().ToLower().Contains(searchSDTUser.ToLower())
                             || u.UserName.ToLower().Contains(searchSDTUser.ToString().ToLower()))
                             && u.SDTUser.ToString().ToLower() != (sdtUserChat.ToString())
                             select new User()
                             {
                                 IdUser = u.IdUser,
                                 UserName = u.UserName,
                                 UserImage = u.UserImage,
                             };
                if(searchSDTUser != null)
                {
                    listUsers = people.ToList();
                }
            
            if(id > 0)
            {
                var messageBoxIdUserReceive = from u in _dbContext.UserEntity
                                              join usr in _dbContext.MyFriendUserEntity
                                              on u.IdUser equals usr.IdUserReceive
                                              where u.IdUser == id
                                              select new User()
                                              {
                                                  IdUserReceive = usr.IdUserReceive,
                                                  IdUser = u.IdUser,
                                                  UserName = u.UserName,
                                                  UserImage = u.UserImage,
                                                  IsMyFriendUser = usr.IsMyFriendUser,
                                                  IdMyFriendUser = usr.IdMyFriendUser
                                              };
                listUserReceive = messageBoxIdUserReceive.ToList();
            }

            var formartPeople = from u in _dbContext.UserEntity
                                join usr in _dbContext.MyFriendUserEntity
                                on u.IdUser equals usr.IdUserReceive
                                select new User()
                                {
                                    IdMyFriendUser = usr.IdMyFriendUser,
                                    IsMyFriendUser = usr.IsMyFriendUser
                                };
            checkFormart = formartPeople.ToList();

            if(listUsers.Count == 0)
            {
                ViewBag.NullsearchUser = "Không tìm thấy người cần tìm...";
            }

            var vm = new PlayMusicProjectMode
            {
                User = listUsers,
                MessageBoxIdUserReceive = listUserReceive,
                CheckFromat = checkFormart,
            };
            return View(vm);
        }

        public IActionResult AddMyFriend(int id)
        {
            int idUserChat = 0;
            int sdtUserChat = 0;
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                     SDTUser = us.SDTUser,
                                 };
                foreach (var us in AcountUser)
                {
                    idUserChat = us.IdUser;
                    sdtUserChat = us.SDTUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }
            

            var edit = _dbContext.MyFriendUserEntity.Find(id);
            edit.IsMyFriendUser = true;


            _dbContext.MyFriendUserEntity.Update(edit);
            _dbContext.SaveChanges();
            return Redirect("/Message/MessageHome");
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody]Message message)
        {
            int idUserChat = 0;
            var x = 0;
            int sdtUserChat = 0;
            bool isCheckChat = true;
            string textMes = message.TextChatMessage;
            textMes = textMes.Trim();
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }
            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                     SDTUser = us.SDTUser,
                                 };
                foreach (var us in AcountUser)
                {
                    idUserChat = us.IdUser;
                    sdtUserChat = us.SDTUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }

            if(!String.IsNullOrEmpty(textMes))
            {
                var addText = new pmoMessageEntity
                {
                    IdUser = idUserChat,
                    IdUserSend = idUserChat,
                    IdUserReceive = message.IdUserReceive,
                    TextChatMessage = textMes,
                    TimeChatMessage = DateTime.Now.ToString("MM/dd h:mm tt"),
                    IsChatMessage = true
                };
                _dbContext.MessageEntity.Add(addText);
                x = _dbContext.SaveChanges();
            }

            var checkUser2 = from us in _dbContext.MessageEntity
                             where us.IdUserSend == idUserChat && us.IdUserReceive == message.IdUserReceive
                             select new Message()
                             {
                                 IdMessage = us.IdMessage,
                                 IdUserSend = us.IdUserSend,
                                 IdUserReceive = us.IdUserReceive,
                             };

            foreach (var item in checkUser2.ToList())
            {
                if(item.IdUserReceive == message.IdUserReceive)
                {
                    isCheckChat = false; break;
                }
            }
            if(checkUser2.ToList().Count() >= 1 && isCheckChat == true)
            {
                var add = new pmoMyFriendUserEntity
                {
                    IdUserSend = message.IdUserReceive,
                    IdUserReceive = idUserChat,
                    IsMyFriendUser = false
                };
                _dbContext.MyFriendUserEntity.Add(add);
                _dbContext.SaveChanges();
            }
            return Ok(x);
        }

        public IActionResult GetListChatMessage()
        {
            int idUserChat = 0;
            int sdtUserChat = 0;
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }
            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                     SDTUser = us.SDTUser,
                                 };
                foreach (var us in AcountUser)
                {
                    idUserChat = us.IdUser;
                    sdtUserChat = us.SDTUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }

            var getList = from us in _dbContext.MessageEntity
                          select new Message()
                          {
                             IdMessage = us.IdMessage,
                             IdUser = idUserChat,
                             IdUserSend = us.IdUserSend,
                             IdUserReceive = us.IdUserReceive,
                             TextChatMessage = us.TextChatMessage,
                             TimeChatMessage = us.TimeChatMessage,
                             IsChatMessage = us.IsChatMessage,
                          };

            return Ok(getList.OrderByDescending(x => x.IdMessage));
        }

        public IActionResult DeleteMyFriend(int id)
        {
            int idUserChat = 0;
            int sdtUserChat = 0;
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                     SDTUser = us.SDTUser,
                                 };
                foreach (var us in AcountUser)
                {
                    idUserChat = us.IdUser;
                    sdtUserChat = us.SDTUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }

            var checkUs = from us in _dbContext.MyFriendUserEntity
                          where us.IdMyFriendUser == id
                          select new MyFriendUser()
                          {
                              IdMyFriendUser = us.IdMyFriendUser,
                              IdUserReceive = us.IdUserReceive,
                              IdUserSend = us.IdUserSend,
                          };
            List<MyFriendUser> myFriendUsers = checkUs.ToList();
            var delMessage = from m in _dbContext.MessageEntity
                             where m.IdUserSend == idUserChat && m.IdUserReceive == myFriendUsers[0].IdUserReceive
                             select new Message()
                             {
                                 IdMessage = m.IdMessage,
                             };

            foreach (var item in delMessage.ToList())
            {
                var delMes = _dbContext.MessageEntity.Find(item.IdMessage);
                _dbContext.MessageEntity.Remove(delMes);
                _dbContext.SaveChanges();
            }

            var del = _dbContext.MyFriendUserEntity.Find(id);
            _dbContext.MyFriendUserEntity.Remove(del);
            _dbContext.SaveChanges();

            return Redirect("/Message/MessageHome");
        }

        public IActionResult DeleteMessageUser(int id)
        {
            int idUserChat = 0;
            int sdtUserChat = 0;
            if (User.Identity.IsAuthenticated)
            {
                foreach (var cclams in User.Claims)
                {
                    _UserNameCookis = cclams.Value;
                }
            }

            if (_UserNameCookis != null)
            {
                var AcountUser = from us in _dbContext.UserEntity
                                 where us.AccountUser == _UserNameCookis
                                 select new User()
                                 {
                                     IdUser = us.IdUser,
                                     UserName = us.UserName,
                                     AccountUser = us.AccountUser,
                                     AccountPass = us.AccountPass,
                                     UserImage = us.UserImage,
                                     IsAdmin = us.IsAdmin,
                                     SDTUser = us.SDTUser,
                                 };
                foreach (var us in AcountUser)
                {
                    idUserChat = us.IdUser;
                    sdtUserChat = us.SDTUser;
                    TempData["IdUser"] = us.IdUser;
                    TempData["UserName"] = us.UserName;
                    TempData["AccountUser"] = us.AccountUser;
                    TempData["AccountPass"] = us.AccountPass;
                    TempData["UserImage"] = us.UserImage;
                    TempData["CheckAdmin"] = us.IsAdmin;
                }
            }

            var checkUs = from us in _dbContext.MyFriendUserEntity
                          where us.IdUserReceive == id
                          select new MyFriendUser()
                          {
                              IdMyFriendUser = us.IdMyFriendUser,
                              IdUserReceive = us.IdUserReceive,
                              IdUserSend = us.IdUserSend,
                          };
            List<MyFriendUser> myFriendUsers = checkUs.ToList();
            var delMessage = from m in _dbContext.MessageEntity
                             where (m.IdUserReceive == id && m.IdUserSend == idUserChat) || (m.IdUserReceive == idUserChat && m.IdUserSend == id)
                             select new Message()
                             {
                                 IdMessage = m.IdMessage,
                             };

            foreach (var item in delMessage.ToList())
            {
                var delMes = _dbContext.MessageEntity.Find(item.IdMessage);
                _dbContext.MessageEntity.Remove(delMes);
                _dbContext.SaveChanges();
            }

            return Redirect("/Message/MessageHome");
        }
    }
}
