﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MindLink.Recruitment.MyChat.Tests
{
    /// <summary>
    /// Tests for the <see cref="ConversationWriter"/>
    /// </summary>
    [TestFixture]
    class ConversationWriterTests
    {
        private IConversationReader reader;
        private IConversationWriter writer;
        private ICommandLineParser cmdParser;
        private ConversationConfig config;

        /// <summary>
        /// Tests that an exception is correctly thrown when an invalid file path is specified.
        /// </summary>
        [Test]
        public void DirectoryNotFound()
        {
            // Arrange
            Reset();
            string[] args = new string[] { "chat.txt", "chat.json" };
            config = cmdParser.ParseCommandLineArguments(args);
            Conversation conversation = reader.ReadConversation(config);

            // Act / Assert
            Assert.That(() => writer.WriteConversation(conversation, @"test\chat.json"),
            Throws.Exception
              .TypeOf<ArgumentException>(), "Argument exception thrown on valid directory");
        }

        /// <summary>
        /// Helper method for preparing core components for use.
        /// </summary>
        private void Reset()
        {
            IReportGenerator reportGenerator = new ReportGenerator();
            reader = new ConversationReader(reportGenerator);
            writer = new ConversationWriter();
            cmdParser = new CommandLineParser();
        }
    }
}