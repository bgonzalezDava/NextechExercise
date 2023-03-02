import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

import { StoryService } from './story.service';

describe('StoryService', () => {
  let service: StoryService;
  let httpClient: HttpClient;

  beforeEach( async () => {
    await TestBed.configureTestingModule({
      imports:[HttpClientModule]
    })
    .compileComponents();

    TestBed.configureTestingModule({});
    httpClient = TestBed.inject(HttpClient);
    service = TestBed.inject(StoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
