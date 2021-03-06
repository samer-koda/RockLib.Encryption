﻿using System;
using System.Collections.Generic;

namespace RockLib.Encryption.Symmetric
{
    /// <summary>
    /// Defines an implementation of <see cref="ICredential"/> that is suitable for initialization
    /// via xml-deserialization.
    /// </summary>
    public class Credential : ICredential
    {
        /// <summary>
        /// Defines the default value of <see cref="SymmetricAlgorithm"/>.
        /// </summary>
        public const SymmetricAlgorithm DefaultAlgorithm = SymmetricAlgorithm.Aes;

        /// <summary>
        /// Defines the default initialization vector size.
        /// </summary>
        public const ushort DefaultIVSize = 16;

        private readonly byte[] _key;

        /// <summary>
        /// Initializes a new instance of the <see cref="Credential"/> class.
        /// </summary>
        /// <param name="algorithm">The <see cref="SymmetricAlgorithm"/> that will be used for a symmetric encryption or decryption operation.</param>
        /// <param name="ivSize">The size of the initialization vector that is used to add entropy to encryption or decryption operations.</param>
        /// <param name="key">The symmetric key returned by the <see cref="GetKey()"/> method.</param>
        public Credential(byte[] key, SymmetricAlgorithm algorithm = DefaultAlgorithm, ushort ivSize = DefaultIVSize)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (key.Length == 0) throw new ArgumentException($"{nameof(algorithm)} must not be empty.", nameof(key));
            if (!Enum.IsDefined(typeof(SymmetricAlgorithm), algorithm)) throw new ArgumentOutOfRangeException(nameof(algorithm), $"{nameof(algorithm)} value is not defined: {algorithm}.");
            if (ivSize <= 0) throw new ArgumentOutOfRangeException(nameof(ivSize), $"{nameof(ivSize)} must be greater than 0.");

            Algorithm = algorithm;
            IVSize = ivSize;
            _key = key;
        }

        /// <summary>
        /// Gets or sets the name that qualifies as a match for this credential info.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the types that qualify as a match for this credential info.
        /// </summary>
        public List<string> Types { get; } = new List<string>();

        IEnumerable<string> ICredentialInfo.Types => Types;

        /// <summary>
        /// Gets the namespaces of types that qualify as a match for this credential info.
        /// </summary>
        public List<string> Namespaces { get; } = new List<string>();

        IEnumerable<string> ICredentialInfo.Namespaces => Namespaces;

        /// <summary>
        /// Gets the <see cref="SymmetricAlgorithm"/> that will be used for a symmetric
        /// encryption or decryption operation.
        /// </summary>
        public SymmetricAlgorithm Algorithm { get; }

        /// <summary>
        /// Gets the size of the initialization vector that is used to add entropy to
        /// encryption or decryption operations.
        /// </summary>
        public ushort IVSize { get; }

        /// <summary>
        /// Gets the plain-text value of the symmetric key that is used for encryption
        /// or decryption operations.
        /// </summary>
        /// <returns>The symmetric key.</returns>
        public byte[] GetKey()
        {
            var copy = new byte[_key.Length];
            _key.CopyTo(copy, 0);
            return copy;
        }
    }
}