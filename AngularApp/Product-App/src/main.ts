
import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { App } from './app/app';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './app/auth-interceptor';

bootstrapApplication(App, {
  providers: [
    provideRouter(routes),
    provideHttpClient(withInterceptorsFromDi()),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
});


// import { bootstrapApplication } from '@angular/platform-browser';
// import { provideHttpClient } from '@angular/common/http';
// import { provideRouter } from '@angular/router';
// import { routes } from './app/app.routes';
// import { App } from './app/app';
// import { AuthInterceptor } from './app/auth-interceptor';
// import { HTTP_INTERCEPTORS } from '@angular/common/http';


// bootstrapApplication(App, {
//   providers: [
//     provideRouter(routes),
//     provideHttpClient(withInterceptorsFromDi()),
//     {
//       provide: HTTP_INTERCEPTORS,
//       useClass: AuthInterceptor,
//       multi: true
//     }
//   ]
// });





// import { bootstrapApplication } from '@angular/platform-browser';
// import { provideHttpClient } from '@angular/common/http';
// import { routes } from './app/app.routes';
// import { App } from './app/app';
// bootstrapApplication(App, {
//   providers: [
//     provideHttpClient()
//   ]
// });


// import { bootstrapApplication } from '@angular/platform-browser';
// import { appConfig } from './app/app.config';
// import { routes } from './app/app.routes';
// import { App } from './app/app';

// bootstrapApplication(App, appConfig)
//   .catch((err) => console.error(err));