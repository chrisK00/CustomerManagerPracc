import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  templateUrl: './test-errors.component.html',
  styleUrls: ['./test-errors.component.css']
})
export class TestErrorsComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  get500Error() {

  }

  get400Error() {

  }

  get401Error() {

  }

  get404Error() {

  }
}
