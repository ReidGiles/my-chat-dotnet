﻿using System.Collections.Generic;

namespace MindLink.Recruitment.MyChat
{
    /// <summary>
    /// Stores conversation configuration data.
    /// </summary>
    public sealed class ConversationConfig
    {
        /// <summary>
        /// The input file path.
        /// </summary>
        public string InputFilePath;

        /// <summary>
        /// The output file path.
        /// </summary>
        public string OutputFilePath { get; set; }

        /// <summary>
        /// Obfuscates user ids if true
        /// </summary>
        public bool ObfuscateUserID { get; set; }

        public IList<IMessageFilter> Filters { get; set; }

        public ConversationConfig()
        {
            Filters = new List<IMessageFilter>();
        }
    }
}
