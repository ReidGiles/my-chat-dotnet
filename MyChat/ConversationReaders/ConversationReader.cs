﻿namespace MindLink.Recruitment.MyChat.ConversationReaders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using MindLink.Recruitment.MyChat.ConversationData;
    using MindLink.Recruitment.MyChat.CommandLineParsing;

    /// <summary>
    /// Responsible for reading conversation data from drive according to <see cref="ConversationConfig"/>
    /// </summary>
    public sealed class ConversationReader : IConversationReader
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ConversationReader"/> class.
        /// </summary>
        public ConversationReader()
        {
        }

        /// <summary>
        /// Reads a conversation from 'configuration.InputFilePath' into a <see cref="Conversation"/> object.
        /// </summary>
        /// <param name="configuration">
        /// The conversation configuration object
        /// </param>
        /// <exception cref="FileNotFoundException">
        /// Thrown when the configuration input file can not be found.
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// Thrown when the configuration input file path is invalid.
        /// </exception>
        /// <exception cref="EndOfStreamException">
        /// Thrown when attempting to read the input file past the end of stream.
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// Thrown when the configuration input file path is to long.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the configuration input file path is not set.
        /// </exception>
        public Conversation ReadConversation(ConversationConfig configuration)
        {
            try
            {
                StreamReader reader = new StreamReader(new FileStream(configuration.InputFilePath, FileMode.Open, FileAccess.Read),
                    Encoding.ASCII);

                string conversationName = reader.ReadLine();
                IList<Message> messages = new List<Message>();
                IList<Message> filteredMessages = new List<Message>();
                string username = string.Empty;

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] split = line.Split(' ');
                    string timestamp = split[0];
                    username = split[1];

                    string content = string.Empty;

                    // Populate content with message body
                    for (int i = 0; i < split.Length - 2; i++)
                    {
                        if (i != 0)
                            content += " ";

                        string word = split[i + 2];

                        content += word;
                    }

                    Message message = new Message { Timestamp = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(timestamp)), SenderId = username, Content = content };
                    messages.Add(message);
                }

                Conversation conversation = new Conversation { Name = conversationName, Messages = messages };
                return conversation;
            }
            catch (FileNotFoundException e)
            {
                throw new ArgumentException("The input file was not found.", configuration.InputFilePath, e);
            }
            catch (DirectoryNotFoundException e)
            {
                throw new ArgumentException("The input file path is invalid", configuration.InputFilePath, e);
            }
            catch (PathTooLongException e)
            {
                throw new ArgumentException("The input file path is too long", configuration.InputFilePath, e);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentException("Configuration input file path must not be null", e);
            }
        }
    }
}