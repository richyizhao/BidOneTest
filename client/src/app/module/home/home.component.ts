import { HttpErrorResponse } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { UserService } from '../../shared/services/user.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  firstName = '';
  lastName = '';
  readonly namePattern = "^[A-Za-z'\\-]+$";
  readonly submittedFullName = signal('');
  readonly saveMessage = signal('');
  readonly saveError = signal('');
  private readonly userService = inject(UserService);

  submitForm() {
    this.saveMessage.set('');
    this.saveError.set('');

    this.userService
      .create({
        firstName: this.firstName,
        lastName: this.lastName,
      })
      .subscribe({
        next: (response) => {
          this.submittedFullName.set(`${response.data.firstName} ${response.data.lastName}`);
          this.saveMessage.set(response.message);
        },

        error: (error: HttpErrorResponse) => {
          const apiError = this.apiErrorMessage(error);

          this.submittedFullName.set('');
          this.saveError.set(apiError);
        },
      });
  }

  private apiErrorMessage(error: HttpErrorResponse): string {
    if (typeof error.error === 'string') {
      return error.error;
    }

    const validationErrors = error.error?.errors;
    if (validationErrors && typeof validationErrors === 'object') {
      const messages = Object.values(validationErrors)
        .flat()
        .filter((message): message is string => typeof message === 'string');

      if (messages.length > 0) {
        return messages[0];
      }
    }

    return 'Unable to save user info. Check that the API is running.';
  }
}
