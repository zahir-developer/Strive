import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class MessageServiceToastr {

  constructor(private toastr: ToastrService) { }
  showMessage(message: any) {
      if (message) {
        const body = message.body;
        const title = message.title;
        switch (message.severity) {
            case 1:     // Success
            case 'success':
            case '1':
                return this.toastr.success(body, title);
            //  break;
            case 2:     // Info
            case '2':
            case 'info':
                return this.toastr.info(body, title);
            // break;
            case 3:     // Warning
            case '3':
            case 'warning':
                return this.toastr.warning(body, title);
            // break;
            case 4:     // Error
            case '4':
            case 'error':
                return this.toastr.error(body, title, ); 
            case 5:     // Hard Stop
            case '5':
                return;
            default:
        }
    
    }
  }
}
