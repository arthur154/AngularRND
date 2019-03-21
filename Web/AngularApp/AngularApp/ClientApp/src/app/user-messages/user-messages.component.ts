import { Input, Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserMessageService } from '../user-message-service/user-message.service';

@Component({
  selector: 'app-user-messages',
  templateUrl: './user-messages.component.html',
  styleUrls: ['./user-messages.component.scss']
})
  
export class UserMessagesComponent implements OnInit {
  subscription: Subscription;

  @Input()
  public alerts: Array<IAlert> = [];

  private backup: Array<IAlert>;
  private currentAlertId: number = 0;

  constructor(private _userMessageService: UserMessageService) {
    this.subscription = this._userMessageService.getUserMessage().subscribe(data => {
      var incomingAlert = data.alert as IAlert;
      var alertType: IAlertType = IAlertType[incomingAlert.type];
      this.addAlert(incomingAlert.message, alertType);
    });
  }

  ngOnInit() {
    this.backup = this.alerts.map((alert: IAlert) => Object.assign({}, alert));
  }

  public addAlert(message: string, alertType: IAlertType) {
    this.alerts.push({
      id: this.currentAlertId++,
      type: alertType,
      message: message,
    });
  }

  public closeAlert(alert: IAlert) {
    const index: number = this.alerts.indexOf(alert);
    this.alerts.splice(index, 1);
  }

  public reset() {
    this.alerts = this.backup.map((alert: IAlert) => Object.assign({}, alert));
  }
}

export interface IAlert {
  id: number;
  type: string;
  message: string;
}

export enum IAlertType {
  info = "info",
  success = "success",
  warning = "warning",
  danger = "danger",
  primaryAlert = "primary",
  secondaryAlert = "secondary",
  light = "light",
  dark = "dark"
}
