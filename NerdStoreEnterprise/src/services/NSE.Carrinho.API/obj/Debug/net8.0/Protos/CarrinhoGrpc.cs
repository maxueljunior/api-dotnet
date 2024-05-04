// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/carrinho.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace NSE.Carrinho.API.Service.gRPC {
  public static partial class CarrinhoCompras
  {
    static readonly string __ServiceName = "CarrinhoAPI.CarrinhoCompras";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::NSE.Carrinho.API.Service.gRPC.ObterCarrinhoRequest> __Marshaller_CarrinhoAPI_ObterCarrinhoRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::NSE.Carrinho.API.Service.gRPC.ObterCarrinhoRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::NSE.Carrinho.API.Service.gRPC.CarrinhoClienteResponse> __Marshaller_CarrinhoAPI_CarrinhoClienteResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::NSE.Carrinho.API.Service.gRPC.CarrinhoClienteResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::NSE.Carrinho.API.Service.gRPC.ObterCarrinhoRequest, global::NSE.Carrinho.API.Service.gRPC.CarrinhoClienteResponse> __Method_ObterCarrinho = new grpc::Method<global::NSE.Carrinho.API.Service.gRPC.ObterCarrinhoRequest, global::NSE.Carrinho.API.Service.gRPC.CarrinhoClienteResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ObterCarrinho",
        __Marshaller_CarrinhoAPI_ObterCarrinhoRequest,
        __Marshaller_CarrinhoAPI_CarrinhoClienteResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::NSE.Carrinho.API.Service.gRPC.CarrinhoReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of CarrinhoCompras</summary>
    [grpc::BindServiceMethod(typeof(CarrinhoCompras), "BindService")]
    public abstract partial class CarrinhoComprasBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::NSE.Carrinho.API.Service.gRPC.CarrinhoClienteResponse> ObterCarrinho(global::NSE.Carrinho.API.Service.gRPC.ObterCarrinhoRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(CarrinhoComprasBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_ObterCarrinho, serviceImpl.ObterCarrinho).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, CarrinhoComprasBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_ObterCarrinho, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::NSE.Carrinho.API.Service.gRPC.ObterCarrinhoRequest, global::NSE.Carrinho.API.Service.gRPC.CarrinhoClienteResponse>(serviceImpl.ObterCarrinho));
    }

  }
}
#endregion
