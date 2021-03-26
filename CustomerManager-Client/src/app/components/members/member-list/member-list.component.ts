import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_interfaces/member';
import { AuthService } from 'src/app/_services/auth.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  members: Member[];

  constructor(private membersService: MembersService, public authService: AuthService) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers() {
    this.membersService.getMembers().subscribe(members => this.members = members),
      error => console.log(error);
  }
}
