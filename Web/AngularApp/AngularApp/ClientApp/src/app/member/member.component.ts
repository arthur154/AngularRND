import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { throwError } from 'rxjs';
import { MemberService } from '../member-service/member.service';
import { UserMessageService } from '../user-message-service/user-message.service';
import { IAlertType } from '../user-messages/user-messages.component';

class Member {
  PersonGUID: string;
  MemberId: number;
  FirstName: string;
  LastName: string;
}

class DataTablesResponse {
  data: any[];
  draw: number;
  recordsFiltered: number;
  recordsTotal: number;
}

@Component({
  selector: 'app-member',
  templateUrl: './member.component.html',
  styleUrls: ['./member.component.scss']
})
export class MemberComponent implements OnInit {
  dtOptions: DataTables.Settings = {};
  members: Member[];
  selectedMember: Member;
  loadingMemeber: boolean = false;

  constructor(private http: HttpClient, private _memberService: MemberService, private _userMessageService: UserMessageService) { }

  ngOnInit(): void {
    const that = this;

    this.dtOptions = {
      dom: 'tir', // Remove this to add back search, pagination, select box, etc.
      pagingType: 'full_numbers',
      lengthMenu: [5, 10, 25, 50],
      pageLength: 5,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback) => {
        that.http
          .post<DataTablesResponse>(
            'api/Member/Search',
            dataTablesParameters, {}
          ).subscribe(resp => {
            that.members = resp.data;

            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: []
            });
          });
      },
      columns: [
        { data: 'MemberId' },
        { data: 'FirstName' },
        { data: 'LastName' },
        { data: 'PersonGUID', orderable: false }
      ]
    };
  }

  selectMember(memberId: any): void {
    this.loadingMemeber = true;
    this._memberService.getMemeberById(memberId).subscribe(
      data => {
        this.loadingMemeber = false;
        this.selectedMember = data as Member;
        return true;
      },
      error => {
        this.loadingMemeber = false;
        return throwError(error);
      }
    );
  }

}
