import { HttpClientModule } from '@angular/common/http';
import { inject, TestBed, waitForAsync } from '@angular/core/testing';

import { FormsDataService } from './forms-data.service';

describe('FormsDataService', () => {
  let service: FormsDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FormsDataService],
      imports: [HttpClientModule]
    });
    service = TestBed.inject(FormsDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should see `json-server`',
    waitForAsync(
      inject([FormsDataService],
        (formsDataService: FormsDataService) => {

          const uri = 'http://localhost:3000/posts/1';
          formsDataService.client.get(uri).subscribe(data => {

            expect(data).not.toBeFalsy('The expected json-server data is not here.');

            const domainData = data as { id: number; title: string; author: string; }
            expect(domainData).not.toBeFalsy('The expected json-server domain data is not here.');

            expect(domainData.title).toEqual('json-server');

          });

        })
    )
  );
});
