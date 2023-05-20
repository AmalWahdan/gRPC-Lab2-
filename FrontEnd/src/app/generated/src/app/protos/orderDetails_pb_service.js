// package: 
// file: src/app/protos/orderDetails.proto

var src_app_protos_orderDetails_pb = require("../../../src/app/protos/orderDetails_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var OrderDetailsService = (function () {
  function OrderDetailsService() {}
  OrderDetailsService.serviceName = "OrderDetailsService";
  return OrderDetailsService;
}());

OrderDetailsService.PostOrder = {
  methodName: "PostOrder",
  service: OrderDetailsService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_orderDetails_pb.OrderRequest,
  responseType: src_app_protos_orderDetails_pb.OrderResponse
};

exports.OrderDetailsService = OrderDetailsService;

function OrderDetailsServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

OrderDetailsServiceClient.prototype.postOrder = function postOrder(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(OrderDetailsService.PostOrder, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.OrderDetailsServiceClient = OrderDetailsServiceClient;

