﻿// Copyright (c) Polyrific, Inc 2018. All rights reserved.

namespace Polyrific.Catapult.Shared.Dto.User
{
    public class RequestTokenDto
    {
        /// <summary>
        /// UserName of the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Authenticator code
        /// </summary>
        public string AuthenticatorCode { get; set; }

        /// <summary>
        /// Recovery code
        /// </summary>
        public string RecoveryCode { get; set; }
    }
}
