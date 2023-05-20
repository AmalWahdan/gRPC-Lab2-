import { OrderRequest } from './generated/src/app/protos/orderDetails_pb.d';
import { Component } from '@angular/core';
import { OrderDetailsService } from './generated/src/app/protos/orderDetails_pb_service';
import { grpc } from '@improbable-eng/grpc-web';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'FrontEnd';
  Message = '';
  Error = false;

 OrderForm(e: any) {
    e.preventDefault();
   let orderMsg = new OrderRequest();

    orderMsg.setUserid (e.target.UserId.value);
    orderMsg.setProductid(e.target.ProductId.value);
    orderMsg.setQuantity(e.target.Quantity.value);
    orderMsg.setAmount(e.target.Price.value);

    grpc.unary(OrderDetailsService.PostOrder, {
      request: orderMsg,
      host: 'https://localhost:7256',
      onEnd: (result) => {
        console.log(result);
        const { Done, Message } = result.message?.toObject() as {
          Done: Boolean;
          Message: string;
        };
        if (!Done) {
          this.Error = false;
        }
        this.Message = Message;
      },
    });
  }


}
