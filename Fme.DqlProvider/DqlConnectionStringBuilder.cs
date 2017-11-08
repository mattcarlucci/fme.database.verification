// ***********************************************************************
// Assembly         : DqlProvider
// Author           : Matt.Carlucci
// Created          : 08-12-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="DqlConnectionStringBuilder.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Documentum.Interop.DFC;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.DqlProvider
{
    /// <summary>
    /// Class KeyValueIndexPair.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class KeyValueIndexPair<T, U>
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public T Key { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public U Value { get; set; }
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }
    }
    /// <summary>
    /// Class DictionaryIndex.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <seealso cref="System.Collections.Generic.List{Fme.DqlProvider.KeyValueIndexPair{T, U}}" />
    public class DictionaryIndex<T, U> : List<KeyValueIndexPair<T, U>>
    {
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="index">The index.</param>
        public virtual void Add(T key, U value, int index)
        {
            var pair = new KeyValueIndexPair<T, U>();
            pair.Key = key;
            pair.Value = value;
            pair.Index = index;
        }
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public virtual void Add(T key, U value)
        {
            var pair = new KeyValueIndexPair<T, U>();
            pair.Key = key;
            pair.Value = value;
            pair.Index = -1;
        }
    }
    /// <summary>
    /// Class DqlConnectionStringBuilder.
    /// </summary>
    /// <seealso cref="System.Data.Common.DbConnectionStringBuilder" />
    public class DqlConnectionStringBuilder : DbConnectionStringBuilder
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnectionStringBuilder"/> class.
        /// </summary>
        public DqlConnectionStringBuilder()
        {
            this.Add("Provider", "DqlDataProvider");
            this.Add("User ID", null);
            this.Add("Password", null);
            this.Add("Data Source", null);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DqlConnectionStringBuilder(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public string Provider { get; set;}

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public string DataSource { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="repository">The repository.</param>
        public DqlConnectionStringBuilder(string user, string password, string repository)
        {
           // Provider = "Dql.Client.Provider";
            DataSource = repository;

            this.Add("Provider", "DqlDataProvider");
            this.Add("User ID", user);
            this.Add("Password", password);
            this.Add("Data Source", repository);
            this.Add("Extended Properties", "");
        }
        
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public String UserId
        {
            get { return this.ContainsKey("User") ? this["User"] as string : this["User Id"] as string; }
        }
        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        public String Password
        {
            get { return this["Password"] as string; }
            set { this["Password"] = value; }
        }
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public String Repository
        {
            get { return this.ContainsKey("Data Source") ? this["Data Source"] as string : this["Repository"] as string; }
        }

        /// <summary>
        /// Sets the extended properties.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void SetExtendedProperties(IDfTypedObject config)
        {            
            foreach (var item in GetExtendedProperties())
            {
                var items = item.Key.Split(new string[] { "[", "]" }, StringSplitOptions.RemoveEmptyEntries);
                if (items.Count() > 1)
                {
                    config.setRepeatingString(items.First().Trim(), int.Parse(items.Last().Trim()), item.Value.Trim());
                }
                else
                    config.setString(item.Key.Trim(), item.Value.Trim());
            }
           
        }
        /// <summary>
        /// Gets the extended properties.
        /// </summary>
        /// <returns>List&lt;KeyValuePair&lt;System.String&gt;&gt;.</returns>
        public List<KeyValuePair<string, string>> GetExtendedProperties()
        {         
            Dictionary<string, string> pairs = new Dictionary<string, string>();

            if (this.ContainsKey("extended properties"))
            {
                string ep = this["extended properties"] as string;
                ep = ep.Trim('\'', '\"');                
                var items = ep.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in items)
                {                   
                    var pair = item.Split(new string[] { "=" }, StringSplitOptions.None);                 
                    pairs.Add(pair.First().Trim(), pair.Last().Trim());
                }
            }
            return pairs.ToList();
        }       

    }
}
