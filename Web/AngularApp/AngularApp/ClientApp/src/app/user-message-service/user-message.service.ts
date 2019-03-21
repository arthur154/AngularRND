import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { IAlert, IAlertType } from '../user-messages/user-messages.component'


@Injectable()
export class UserMessageService {
  private subject = new Subject<any>();

  constructor() { }

  addUserMessage(message: string, alertType: IAlertType) {
    var alert: IAlert = {
      id: 0,
      message: message,
      type: alertType
    };
    
    this.subject.next({ alert: alert });
  }

  getUserMessage(): Observable<any> {
    return this.subject.asObservable();
  }
}
