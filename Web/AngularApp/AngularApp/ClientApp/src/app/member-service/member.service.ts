import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class MemberService {

  constructor(private http: HttpClient) { }

  getSingleMemeber() {
    return this.http.get('/api/member', httpOptions);
  }

  getMemeberList() {
    return this.http.get('/api/member/list', httpOptions);
  }

  getMemeberById(id: string) {
    return this.http.get('/api/member/' + id, httpOptions);
  }

  insertMember(memberJson: object): Observable<string> {
    return this.http.post<string>('/api/member', memberJson, httpOptions);
  }

  getInsertStatus(id: string) {
    return this.http.get('/api/member/status/' + id, httpOptions);
  }
}
