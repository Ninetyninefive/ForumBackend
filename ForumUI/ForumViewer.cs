using System;
using ForumLibrary;
using System.Collections.Generic;
using System.Linq;

namespace ForumUI
{
    public class ForumViewer
    {
        
        public ForumViewer()
        {

        }

        public Users CreateUser()
        {
            var newUser = new Users();
            Console.Clear();
            Console.WriteLine("Enter nickname:");
            var inputNewNickname = Console.ReadLine();

            Console.WriteLine("Enter first name:");
            var inputNewFirstName = Console.ReadLine();

            Console.WriteLine("Enter last name:");
            var inputNewLastName = Console.ReadLine();

            newUser.nickName = inputNewNickname;
            newUser.FirstName = inputNewFirstName;
            newUser.LastName = inputNewLastName;

            return newUser;
        }

        public Topics CreateTopic(Users currentUser)
        {
            
            Console.Clear();
            Console.WriteLine("Enter TOPIC name:");
            var inputNewTopicName = Console.ReadLine();

            Console.WriteLine("Enter DESCRIPTION:");
            var inputNewTopicDescription = Console.ReadLine();

            var newTopic = new Topics();

            newTopic.ownerId = currentUser.userId;
            newTopic.name = inputNewTopicName;
            newTopic.description = inputNewTopicDescription;
            newTopic.visible = 1;
            
            return newTopic;
        }

        public Threads CreateThread(Topics currentTopic, Users currentUser)
        {

            Console.Clear();
            Console.WriteLine("Enter thread name:");
            var inputNewThreadName = Console.ReadLine();

            Console.WriteLine("Enter thread subject:");
            var inputNewThreadSubject = Console.ReadLine();

            var newThread = new Threads();

            newThread.topicId = currentTopic.topicId;
            newThread.ownerId = currentUser.userId;
            newThread.subject = inputNewThreadSubject;
            newThread.visible = 1;

            return newThread;
        }

        public Messages EditMessage(Messages messageToEdit, Users currentUser)
        {

            Console.Clear();
            Console.WriteLine("Enter NEW message:");
            
            var messageToInsert = Console.ReadLine();
            messageToEdit.message = messageToInsert;
            messageToEdit.ownerId = currentUser.userId;
            
            return messageToEdit;
        }
        public Messages CreateMessage(Threads currentThread, Users currentUser)
        {

            Console.Clear();
            Console.WriteLine("Enter message:");
            var inputNewMessage = Console.ReadLine();

            var newMessage = new Messages();

            newMessage.threadId = currentThread.threadId;
            newMessage.ownerId = currentUser.userId;

            newMessage.message = inputNewMessage;
            newMessage.visible = 1;

            return newMessage;
        }
        public void Run()
        {
            var messageMenu = "";
            Users currentUser = null;
            Topics currentTopic = null;
            Threads currentThread = null;
            Messages currentMessage = null;

            var repo = new SQLiteForumRepository();
            
            var users = repo.GetUsers();
            var topics = repo.GetTopics();
            var threads = repo.GetThreads();
            var messages = repo.GetMessages();

            if(users.Count() == 0)
            {
                var defUser = CreateUser();
                repo.AddUser(defUser);
                users = repo.GetUsers();
            }

            Console.WriteLine("\n\nWelcome to ForumViewer ['back' to go back]");
            Console.WriteLine("Who are you? (enter nickname) [or type 'create' to add new forum user]\n");
            Console.WriteLine("UserList [create - back - delete]\n");
            foreach (var item in users)
            {
                    Console.WriteLine($"Nickname:{item.nickName} (UserID: {item.userId} Full Name[{item.FirstName} {item.LastName}] Joined: {item.dateCreated})");
            }
            while (currentUser == null)
            {

                var inputName = Console.ReadLine();
                
                    if (inputName == "create")
                    {
                        var newUser = CreateUser();
                        repo.AddUser(newUser);
                        users = repo.GetUsers();
                    }
                    if(inputName == "back")
                    {
                        currentUser = null;
                    }
                    if (inputName == "delete")
                    {
                        Console.WriteLine("\nEnter user ID to delete");
                        var deletionId = Console.ReadLine();
                        foreach (var item in users)
                        {
                            if(item.userId == Convert.ToInt32(deletionId))
                            {
                                repo.DeleteUser(item);
                                currentUser = null;
                                currentTopic = null;
                                currentThread = null;
                                currentMessage = null;
                                
                            }
                        }
                        users = repo.GetUsers();
                     }
                foreach (var user in users)
                {
                    if (inputName == user.nickName || inputName == user.userId.ToString())
                    {
                        currentUser = user;
                    }
                    else
                    {
                        messageMenu = "Try again. [Enter user nickname or ID] or type 'create' to create a new user.";
                        inputName = Console.ReadLine();
                    }
                }
            }
            Console.Clear();
            Console.WriteLine($"Logged on as: {currentUser.nickName} ID: {currentUser.userId}");

            messageMenu = "Select Topic [create - back - delete]\n";
            Console.WriteLine(messageMenu);
            Console.WriteLine($"\n\nActive Topics:");
            foreach (var topic in topics)
            {
                if (topic.visible == 1)
                    Console.WriteLine($"TOPIC: {topic.name}  (ID: {topic.topicId} Description:{topic.description} [Owner:{topic.ownerId} Created:{topic.dateCreated}])");
            }
            while (currentTopic == null)
            {
                var inputTopicName = Console.ReadLine();
                
                    if (inputTopicName == "create")
                    {
                        var newTopic = CreateTopic(currentUser);
                        repo.NewTopic(newTopic);
                        currentTopic = null;
                        currentThread = null;
                        currentMessage = null;
                        topics = repo.GetTopics(); 
                    }
                    if(inputTopicName == "back")
                    {
                        currentTopic = null;
                    }
                    if(inputTopicName == "delete")
                    {
                        Console.WriteLine("Enter TOPIC ID to delete");
                        var deletionId = Console.ReadLine();
                        foreach (var item in topics)
                        {
                            if (item.topicId == Convert.ToInt32(deletionId))
                            {
                                repo.DeleteTopic(item);
                                currentTopic = null;
                                currentThread = null;
                                currentMessage = null;
                                
                            }
                        }
                        topics = repo.GetTopics();
                     }

                foreach (var topic in topics)
                {
                    if (inputTopicName == topic.name || inputTopicName == topic.topicId.ToString())
                    {
                        currentTopic = topic;
                    }
                    else
                    {
                        messageMenu = "Try again. [enter ID to select --  'create' -- 'back' -- 'delete']";
                        inputTopicName = Console.ReadLine();
                    }
                }
            }

            Console.Clear();
            Console.WriteLine($"Browsing as: {currentUser.nickName} [ID: {currentUser.userId}]");
            Console.WriteLine($"Selected Topic ID:{currentTopic.topicId} Name:{currentTopic.name} Desc:{currentTopic.description} Created:{currentTopic.dateCreated}");

            messageMenu = "Select Thread [create - back - delete]\n";
            Console.WriteLine(messageMenu);
            Console.WriteLine($"\n\nActive Threads:");
            foreach (var thread in threads)
            {
                if (thread.topicId == currentTopic.topicId)
                    Console.WriteLine($"[{thread.threadId}] Subject: {thread.subject} (Owner:{thread.ownerId})");
            }
            while (currentThread == null)
            {
                var inputThreadName = Console.ReadLine();
                
                    if (inputThreadName == "create")
                    {
                        var newThread = CreateThread(currentTopic, currentUser);
                        repo.NewThread(newThread);
                        currentThread = null;
                        currentMessage = null;
                        threads = repo.GetThreads();
                }
                    if (inputThreadName == "back")
                    {
                        currentThread = null;
                    }
                    if (inputThreadName == "delete")
                    {
                        Console.WriteLine("Enter THREAD ID to delete");
                        var deletionId = Console.ReadLine();
                        foreach (var item in threads)
                        {
                            if (item.threadId == Convert.ToInt32(deletionId))
                            {
                                repo.DeleteThread(item);
                                currentThread = null;
                                currentMessage = null;
                            }
                        }
                    threads = repo.GetThreads();
                }
                foreach (var thread in threads)
                {
                    if (inputThreadName == thread.subject || inputThreadName == thread.threadId.ToString())
                    {
                        currentThread = thread;
                    }
                    else
                    {
                        messageMenu = "Try again. [Enter thread name or ID to select or 'create' to start new thread.]";
                        inputThreadName = Console.ReadLine();
                    }
                }
            }


            Console.Clear();
            Console.WriteLine($"Logged on as: {currentUser.nickName} ID: {currentUser.userId}");
            Console.WriteLine($"Topic Name:{currentTopic.name}ID:{currentTopic.topicId}  Desc:{currentTopic.description} Created:{currentTopic.dateCreated}");
            Console.WriteLine($"Thread Subject:{currentThread.subject}ID:{currentThread.threadId}  Desc:{currentThread.ownerId} Created:{currentThread.lastPostDate}");

            messageMenu = "Select Message [create - back - delete]\n";
            Console.WriteLine(messageMenu);
            Console.WriteLine($"\n\nActive Messages:");
            foreach (var message in messages)
            {
                if (message.threadId == currentThread.threadId)
                    Console.WriteLine($"[{message.messageId}] {message.message} [{message.ownerId} {message.dateCreated}]");
            }

            messageMenu = "'create' // 'edit' // 'delete'\n";
            Console.WriteLine(messageMenu);
            while (currentMessage == null)
            {
                var inputMessageName = Console.ReadLine();
                
                    if (inputMessageName == "create")
                    {
                        var newMessage = CreateMessage(currentThread, currentUser);
                        repo.NewMessage(newMessage);
                        currentMessage = null;
                        messages = repo.GetMessages();
                    }
                    if (inputMessageName == "back")
                    {
                        currentMessage = null;
                    }
                    if (inputMessageName == "delete")
                    {
                        Console.WriteLine("Enter THREAD ID to delete");
                        var deletionId = Console.ReadLine();
                        foreach (var item in messages)
                        {
                            if (item.messageId == Convert.ToInt32(deletionId))
                            {
                                repo.DeleteMessage(item);
                                currentMessage = null;
                            }
                        }
                        messages = repo.GetMessages();
                    }
                    if (inputMessageName == "edit")
                    {
                        Console.WriteLine("Enter message ID to edit");
                        var editId = Console.ReadLine();
                        foreach (var item in messages)
                        {
                            if (item.messageId == Convert.ToInt32(editId))
                            {
                                var editMessage = EditMessage(item, currentUser);
                                repo.EditMessage(editMessage);
                                currentMessage = null;
                            }
                        }
                    messages = repo.GetMessages();
                    }
                foreach (var message in messages)
                {
                    if (inputMessageName == message.message || inputMessageName == message.messageId.ToString())
                    {
                        currentMessage = message;
                    }
                    else
                    {
                        messageMenu = "Try again. [Enter thread name or ID to select or 'create' to start new thread.]";
                        inputMessageName = Console.ReadLine();
                    }
                }
            }
        }
    }
}
