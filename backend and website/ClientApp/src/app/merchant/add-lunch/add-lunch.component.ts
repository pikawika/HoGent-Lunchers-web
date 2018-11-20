import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-lunch',
  templateUrl: './add-lunch.component.html',
  styleUrls: ['./add-lunch.component.css']
})
export class AddLunchComponent implements OnInit {

  public lunch: FormGroup;
  public errorMsg: string;

  constructor(
    private fb: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.lunch = this.fb.group({
      name: [
        '',
        [Validators.required, Validators.minLength(4)]
      ],
      price: [
        '',
        [Validators.required]
      ],
      description: [
        '',
        [Validators.required, Validators.minLength(10)]
      ],
      startdate: [
        '',
        [Validators.required]
      ],
      enddate: [
        '',
        [Validators.required]
      ],
      ingredienten: [
        '',
        [Validators.required]
      ],
      tags: [
        '',
        [Validators.required]
      ]
    });
  }

  onSubmit() {
    
  }
}

