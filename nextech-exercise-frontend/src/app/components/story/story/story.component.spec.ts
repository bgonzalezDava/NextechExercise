import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from 'src/app/app.component';
import { StoryService } from 'src/app/services/story-service/story.service';

import { StoryComponent } from './story.component';

describe('StoryComponent', () => {
  let component: StoryComponent;
  let httpClient: HttpClient;
  let storyService: StoryService;
  let fixture: ComponentFixture<StoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports:[HttpClientModule],
      declarations: [ StoryComponent ]
    })
    .compileComponents();

    httpClient = TestBed.inject(HttpClient);
    storyService = TestBed.inject(StoryService);
    fixture = TestBed.createComponent(StoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('Page number is 1 when just initialized', () => {
    expect(component.pageNumber).toBe(1);
  });

  it('Page number increase by 1 when click "Next" button', () =>{
    let initPageNumber = component.pageNumber;
    component.nextPage();
    expect(component.pageNumber).toBe(initPageNumber + 1);
  });

  it('Page number decrease by 1 when click "Previous" button', () =>{
    let initPageNumber = component.pageNumber;
    component.previousPage();
    expect(component.pageNumber).toBe(initPageNumber - 1);
  });
});
