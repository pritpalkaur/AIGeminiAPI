import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SecureTestService } from '../services/secure-service';

@Component({
  selector: 'app-secure-test',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './secure-test.html',
  styleUrl: './secure-test.css'
})

export class SecureTest {
message: string = '';
  user: string = '';
  time: string = '';

  constructor(private secureService: SecureTestService) {}

  ngOnInit() {
    this.secureService.getHelloMessage().subscribe({
      next: (data) => {
        this.message = data.message;
        this.user = data.user;
        this.time = data.time;
      },
      error: (err) => console.error('Error loading secure message', err)
    });
  }
}

