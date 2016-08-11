using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WeerutTestWCFService.DTO
{
    [DataContract(Name = "response")]
    public class Response
    {

        [DataMember(Name = "fund", EmitDefaultValue = false)]
        public Fund Fund { get; private set; }

        [DataMember(Name = "funds", EmitDefaultValue = false)]
        public List<Fund> Funds { get; private set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; private set; }

        [DataMember(Name = "error", EmitDefaultValue = false)]
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// The constructor is private and not allowed to be called by other classes.
        /// Please use the static methods to initiate Response instance.
        /// </summary>
        private Response()
        {
        }

        /// <summary>
        /// Create Response instance with a single entry of Fund
        /// </summary>
        /// <param name="fund">A single entry of Fund</param>
        /// <returns>Response instance</returns>
        public static Response CreateResponse(Fund fund)
        {
            return new Response() { Fund = fund };
        }

        /// <summary>
        /// Create Response instance with a list of Fund
        /// </summary>
        /// <param name="funds">A list of Fund</param>
        /// <returns>Response instance</returns>
        public static Response CreateResponse(List<Fund> funds)
        {
            return new Response() { Funds = funds };
        }

        /// <summary>
        /// Create Response instance with a message string
        /// </summary>
        /// <param name="message">A message string</param>
        /// <returns>Response instance</returns>
        public static Response CreateResponse(string message)
        {
            return new Response() { Message = message };
        }

        /// <summary>
        /// Create Response instance from an exception
        /// </summary>
        /// <param name="ex">an exception</param>
        /// <returns>Response instance</returns>
        public static Response CreateResponse(Exception ex)
        {
            return new Response() { ErrorMessage = ex.Message };
        }

    }
}
